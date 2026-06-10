using UnityEngine;

public class Board 
{
     public Tile[,]  tiles;

     public Board(int width, int height)
     {
          tiles = new Tile[width, height];
     }
     
     public Tile getTile(Vector2Int coordinates)
     {
          return tiles[coordinates.x, coordinates.y];
     }
}