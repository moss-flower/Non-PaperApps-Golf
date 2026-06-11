using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Board Related Stuff
    [SerializeField] private BoardFactory boardFactory;
    [SerializeField] private Vector2Int boardSize = new Vector2Int(26, 16);
    public Board board { get; private set; }
    
    // Golf ball related stuff
    [SerializeField] private GameObject golfBallPrefab;
    private GolfBall golfBall;
    
    // Rolling related stuff
    private DiceRoller diceRoller;
    
    private List<Tile> activeTiles = new List<Tile>();

    
    

    private void Awake()
    {
        diceRoller = new DiceRoller();
    }

    private void Start()
    {
        // Building board data and visuals
        GameObject boardRoot = new GameObject("Board");
        board = boardFactory.CreateBoard(boardSize.x, boardSize.y, boardRoot.transform);
        boardRoot.transform.position = calculateBoardOffset(boardSize);
        
        // Building Golf Ball Data
        // Will need to get info about start position etc later
        GameObject ball = Instantiate(golfBallPrefab, board.tiles[0, 0].transform.position, new Quaternion());
        golfBall = ball.GetComponent<GolfBall>();
        
        // Event Subscriptions
        EventManager.instance.OnTileClick += MoveEvent;
        EventManager.instance.OnRoleClick += HandleRoll;
    }

    private void MoveEvent(int x, int y)
    {
        if (activeTiles.Count > 0)
        {
            resetTiles();
        }
        Vector3 newPosition = board.tiles[x, y].transform.position;
        golfBall.move(newPosition, new Vector2Int(x,y));
    }

    private Vector3 calculateBoardOffset(Vector2Int boardSize)
    {
        float x  = (boardSize.x * 0.5f) * 0.5f;
        float y  = (boardSize.y * 0.5f) * 0.5f;
        return new Vector3(-x, -y, 0);
    }

    private void HandleRoll()
    {
        int roll = diceRoller.Roll();
        print("Roll: " + roll);
        checkAvailableTiles(roll);
    }

    private void checkAvailableTiles(int roll)
    {
        Vector2Int root = golfBall.boardPosition;
        activeTiles.Clear();
        foreach (Vector2Int direction in orthogonalDirections)
        {
            Vector2Int pos = root + (direction * roll);
            
            if (board.isInBounds(pos))
            {
                print("Checking position: " + pos);
                Tile tile = board.getTile(pos);
                tile.makeClickable();
                activeTiles.Add(tile);
            }
            
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