using UnityEngine;
using Random = System.Random;

public class BoardFactory : MonoBehaviour
{
    //public GameObject[] tilePrefabs {get; private set;}
    public GameObject tilePrefab;
    
    public Board CreateBoard(int width, int height)
    {
        Random random = new Random();
        Board board = new Board(width, height);
        //int size = tilePrefabs.Length;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                //GameObject obj = Instantiate(tilePrefabs[random.Next(size)], new Vector3(i,0,j), Quaternion.identity);
            }
        }
    }
}