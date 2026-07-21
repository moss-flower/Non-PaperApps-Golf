using System;
using System.Collections.Generic;
using Builders;
using Factories;
using Helpers;
using Models;
using UnityEngine;

namespace Managers
{
    /// <summary>
    /// Primary class for keeping track of the game state, moving pieces, and loading the board.
    /// </summary>
    /// <remarks>
    /// This class has too many responsibilities and should be refactored to make its purpose more clear.
    /// </remarks>
    public class GameManager : MonoBehaviour
    {
        // Board Related Stuff
        [SerializeField] private BoardFactory boardFactory;
        [SerializeField] private Vector2Int boardSize = new Vector2Int(26, 16);
        private Board board { get; set; }
        [SerializeField] private string boardName;
        private GameObject boardRoot;
    
        public GameState gameState { get; private set; } = new GameState();
    
        // Golf ball related stuff
        [SerializeField] private GameObject golfBallPrefab;
        private GameObject golfBallGameObject;
        private GolfBall golfBall;
    
        // Rolling related stuff
        private DiceRoller diceRoller;
        private List<Tile> activeTiles = new List<Tile>();
    
        //Other stuff
        [SerializeField] private ArrowBuilder arrowBuilder;

        private bool pauseState = false;
    
        // Events
        public static event Action OnGameStart;
        public event Action<int> OnScoreChanged;
        public event Action<int, int> OnRoll;
        public event Action OnRoundEnd;
        public event Action OnPause;

        /// <summary>
        /// A method through which other classes can query the current running state of the game.
        /// </summary>
        /// <remarks>
        /// Only used by a single class. Should be refactored to rely solely on the Pause Event.
        /// </remarks>
        /// <returns>A boolean result.</returns>
        public bool IsPaused()
        {
            return pauseState;
        }

        /// <summary>
        /// Method used for toggling the state of the game.
        /// </summary>
        public void TogglePause()
        {
            if (!gameState.HasStarted())
            {
                return;
            }
            pauseState = !pauseState;
            OnPause?.Invoke();
        }

        private void Awake()
        {
            diceRoller = new DiceRoller();
        }

        /// <summary>
        /// Method used to initiate a game.
        /// Builds the necessary pieces to allow the game to be played, and sends an event to notify other systems
        /// that the game is ready.
        /// </summary>
        /// <param name="levelPath">The file path to the level to be loaded.</param>
        public void Load(string levelPath)
        {
            boardName = levelPath;
            // Building board data and visuals
            if (boardRoot != null)
            {
                Destroy(boardRoot);
            }
            boardRoot = new GameObject("Board");
            //board = boardFactory.CreateBoard(boardSize.x, boardSize.y, boardRoot.transform);
            board = boardFactory.LoadBoardFromFile(boardName, boardRoot.transform);
            boardRoot.transform.position = calculateBoardOffset(boardSize);
        
            // Building Golf Ball Data
            // Will need to get info about start position etc later

            if (golfBallGameObject != null)
            {
                Destroy(golfBallGameObject);
            }
            golfBallGameObject = Instantiate(golfBallPrefab, board.tiles[0,0].transform.position, new Quaternion());
            golfBall = golfBallGameObject.GetComponent<GolfBall>();
            MoveEvent(board.startTileLocation.x, board.startTileLocation.y);
        
            gameState.startGame();
            arrowBuilder.Initialize();
        
        
            // Event Subscriptions
            //Order matters here. If the game is not started, the game UI won't be enabled and cannot be updated
            //This causes it to render the last rendered score, which is silly.
            OnGameStart?.Invoke();
            OnScoreChanged?.Invoke(gameState.getScore());
        }

        /// <summary>
        /// A public method for moving a game piece. Called by the event handler to notify the game manager that a piece has been moved.
        /// </summary>
        /// <remarks>Could likely invert the relationship (have the Game manager query the event handler and wait for a response).</remarks>
        /// <param name="x">The x coordinate on the board of the selected tile</param>
        /// <param name="y">The y coordinate on the board of the destination tile</param>
        public void MoveEvent(int x, int y)
        {
            Vector3 currentPosition = golfBall.transform.position;
        
            if (activeTiles.Count > 0)
            {
                resetTiles();
            }
            Tile selectedTile = board.tiles[x, y];
            print("Moving to tile: " + selectedTile.tileDefinition.name + " at position: " + new Vector2Int(x, y));
            Vector3 newPosition = selectedTile.transform.position;
            golfBall.move(newPosition, new Vector2Int(x,y));
            applyTileEffect(selectedTile);

            if (arrowBuilder.IsInitialized())
            {
                arrowBuilder.AddArrow(currentPosition, newPosition);
            }
        
        
            gameState.incrementScore();
            if (OnScoreChanged != null)
            {
                OnScoreChanged.Invoke(gameState.getScore());
            }
        
            if (selectedTile.tileDefinition.isWinningTile)
            {
                gameState.endGame();
                arrowBuilder.RemoveArrows();
                OnRoundEnd?.Invoke();
            }
        
        }

        private Vector3 calculateBoardOffset(Vector2Int boardSize)
        {
            float x  = (boardSize.x * 0.5f) * 0.5f;
            float y  = (boardSize.y * 0.5f) * 0.5f;
            return new Vector3(-x, -y, 0);
        }
    
        /// <summary>
        /// A method through which the players interaction performs a dice roll.
        /// </summary>
        public void HandleRoll()
        {
            if (gameState.HasRolled() && gameState.RemainingMulligans <= 0)
            {
                return;
            } 
            if (gameState.HasRolled() && gameState.RemainingMulligans >= 1)
            {
                gameState.decrementMulligans();
            }
            if (!gameState.HasRolled())
            {
                gameState.setHasRolled(true);
            }
        
            int roll = diceRoller.Roll() + golfBall.modifier;
            OnRoll?.Invoke(roll - golfBall.modifier, gameState.RemainingMulligans);
            print("Roll: " + roll);
            resetTiles();
            checkAvailableTiles(roll);
        
        }

        /// <summary>
        /// Method used to determine which tiles are targeted after a given roll.
        /// Takes the result of a roll (plus modifiers), and for the 8 orthogonal directions,
        /// steps through each until it's reached the destination/target, updating the state as it passes through.
        /// </summary>
        /// <param name="roll">An integer representing the players roll</param>
        private void checkAvailableTiles(int roll)
        {
            Vector2Int root = golfBall.boardPosition;
            // need to add a proper step-through system and a check to see if the ball
            // can currently hop trees or not (eg, is on the fairway). 
            foreach (Vector2Int direction in orthogonalDirections)
            {
                bool isBlocked = false;
            
                //first check whether or not we can put, and if we can, wether the put is a playable tile.
                Vector2Int pos = root + (direction);
                if (golfBall.canPutt)
                {
                    if (board.IsInBoundsAndIsTartetable(pos))
                    {
                        makeClickableAndAddTile(board.GetTile(pos));
                    }
                }
            
                // First check whether the final spot is even a valid target, if not, continue.
                pos  = root + (direction * roll);
                if (!board.IsInBoundsAndIsTartetable(pos))
                {
                    continue;
                }
            
                // If it IS targetable, then check if we can shoot over trees.
                // If we can, get the tile and move on.
                // If we can't shoot over trees, check if there are any trees in the way.

                if (golfBall.canHopWalls)
                {
                    makeClickableAndAddTile(board.GetTile(pos));
                    continue;
                }
            
                for (int i = 0; i < roll; i++)
                {
                    pos = root + (direction * i);
                    if (board.GetTile(pos).tileDefinition.isTall)
                    {
                        isBlocked = true;
                        break;
                    }
                }
                if (isBlocked) continue;
                pos = root + (direction * roll);
                makeClickableAndAddTile(board.GetTile(pos));
            
            }
        }

        /// <summary>
        /// A method used to set currently active tiles (tiles which can be targeted) to be inactive (no longer targetable).
        /// </summary>
        private void resetTiles()
        {
            foreach (Tile tile in activeTiles)
            {
                tile.makeUnclickable();
            }
            activeTiles.Clear();
        }

        /// <summary>
        /// A method called after a tile has been selected to apply the intended effects of the ball landing at a
        /// given location.
        /// </summary>
        /// <param name="tile">The tile at the location in which the ball landed.</param>
        private void applyTileEffect(Tile tile)
        {
            TileDefinition definition = tile.tileDefinition;
            golfBall.modifier =  definition.modifier;
            golfBall.canPutt = definition.isPuttable;
            golfBall.canHopWalls = definition.isClearView;
        }

        /// <summary>
        /// A method used as part of the "checkAvailableTiles" call when a possible move has been identified.
        /// </summary>
        /// <param name="tile"></param>
        private void makeClickableAndAddTile(Tile tile)
        {
            tile.makeClickable();
            activeTiles.Add(tile);
        }
    
        /// <summary>
        /// An array of directions used in the "checkAvailableTiles" method.
        /// </summary>
        private static readonly Vector2Int[] orthogonalDirections =
        {
            Vector2Int.up,
            Vector2Int.down,
            Vector2Int.left,
            Vector2Int.right,
            new Vector2Int(1, -1),
            new Vector2Int(1, 1),
            new Vector2Int(-1, -1),
            new Vector2Int(-1, 1),
        };
    }
}