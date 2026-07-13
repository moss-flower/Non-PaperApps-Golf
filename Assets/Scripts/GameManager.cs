using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Board Related Stuff
    [SerializeField] private BoardFactory boardFactory;
    [SerializeField] private Vector2Int boardSize = new Vector2Int(26, 16);
    public Board board { get; private set; }
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
    
    // Events
    public static event Action OnGameStart;
    public event Action<int> OnScoreChanged;
    public event Action<int> OnRoll;
    public event Action OnRoundEnd;
    

    private void Awake()
    {
        diceRoller = new DiceRoller();
    }

    private void Start()
    {
        
    }

    private void OnEnable()
    {
        EventManager.instance.OnTileClick += MoveEvent;
        EventManager.instance.OnRoleClick += HandleRoll;
    }

    private void OnDisable()
    {
        EventManager.instance.OnTileClick -= MoveEvent;
        EventManager.instance.OnRoleClick -= HandleRoll;
    }

    public void Load(string level)
    {
        boardName = level;
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
        
        gameState.decrementScore();
        
        // Event Subscriptions
        
        
        OnGameStart?.Invoke();
    }

    private void MoveEvent(int x, int y)
    {
        if (activeTiles.Count > 0)
        {
            resetTiles();
        }
        Tile selectedTile = board.tiles[x, y];
        print("Moving to tile: " + selectedTile.tileDefinition.name + " at position: " + new Vector2Int(x, y));
        Vector3 newPosition = selectedTile.transform.position;
        golfBall.move(newPosition, new Vector2Int(x,y));
        applyTileEffect(selectedTile);
        
        gameState.incrementScore();
        if (OnScoreChanged != null)
        {
            OnScoreChanged.Invoke(gameState.getScore());
        }
        
        if (selectedTile.tileDefinition.isWinningTile)
        {
            OnRoundEnd?.Invoke();
        }
        
    }

    private Vector3 calculateBoardOffset(Vector2Int boardSize)
    {
        float x  = (boardSize.x * 0.5f) * 0.5f;
        float y  = (boardSize.y * 0.5f) * 0.5f;
        return new Vector3(-x, -y, 0);
    }

    private void HandleRoll()
    {
        int roll = diceRoller.Roll() + golfBall.modifier;
        OnRoll?.Invoke(roll - golfBall.modifier);
        print("Roll: " + roll);
        resetTiles();
        checkAvailableTiles(roll);
    }

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
                if (board.isInBoundsAndIsTartetable(pos))
                {
                    makeClickableAndAddTile(board.getTile(pos));
                }
            }
            
            // First check whether the final spot is even a valid target, if not, continue.
            pos  = root + (direction * roll);
            if (!board.isInBoundsAndIsTartetable(pos))
            {
                continue;
            }
            
            // If it IS targetable, then check if we can shoot over trees.
            // If we can, get the tile and move on.
            // If we can't shoot over trees, check if there are any trees in the way.

            if (golfBall.canHopWalls)
            {
                makeClickableAndAddTile(board.getTile(pos));
                continue;
            }
            
            for (int i = 0; i < roll; i++)
            {
                pos = root + (direction * i);
                if (board.getTile(pos).tileDefinition.isTall)
                {
                    isBlocked = true;
                    break;
                }
            }
            if (isBlocked) continue;
            pos = root + (direction * roll);
            makeClickableAndAddTile(board.getTile(pos));
            
        }
    }

    private void resetTiles()
    {
        foreach (Tile tile in activeTiles)
        {
            tile.makeUnclickable();
        }
        activeTiles.Clear();
    }

    private void applyTileEffect(Tile tile)
    {
        TileDefinition definition = tile.tileDefinition;
        golfBall.modifier =  definition.modifier;
        golfBall.canPutt = definition.isPuttable;
        golfBall.canHopWalls = definition.isClearView;
    }

    private void makeClickableAndAddTile(Tile tile)
    {
        tile.makeClickable();
        activeTiles.Add(tile);
    }
    
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