using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class BoardFactory : MonoBehaviour
{
    public GameObject tilePrefab;
    public TileDefinition[] tileDefinitions;
    
    public Board CreateBoard(int width, int height, Transform parent)
    {
        //somewhere in here we'll have to take some sort of input so we can 
        //automatically generate the board with the correct definitions
        //probably json or something.
        Board board = new Board(width, height);
        int size = tileDefinitions.Length;
        Random random = new Random();
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                GameObject tile = Instantiate(tilePrefab, new Vector3(i*0.5f, j*0.5f, 0), Quaternion.identity, parent);
                Tile tileComponent = tile.GetComponent<Tile>();
                tileComponent.Initialize(tileDefinitions[random.Next(tileDefinitions.Length)], i, j);
                board.tiles[i, j] = tileComponent;
            }
        }
        return board;
    }
}