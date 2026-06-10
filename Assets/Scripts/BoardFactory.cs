using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class BoardFactory : MonoBehaviour
{
    //public GameObject[] tilePrefabs {get; private set;}
    public GameObject tilePrefab;
    public TileDefinition[] tileDefinitions;
    
    public Board CreateBoard(int width, int height)
    {
        Board board = new Board(width, height);
        int size = tileDefinitions.Length;
        Random random = new Random();
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                GameObject tile = Instantiate(tilePrefab, new Vector3(i, j, 0), Quaternion.identity, transform);
                Tile tileComponent = tile.GetComponent<Tile>();
                tileComponent.Initialize(tileDefinitions[random.Next(tileDefinitions.Length)]);
                board.tiles[i, j] = tileComponent;
                
            }
        }
        return board;
    }
}