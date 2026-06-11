using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Board 
{
     public Tile[,]  tiles;
     public List<Tile> highLightedTiles  = new List<Tile>();
     private Vector2Int dimensions;
     

     public Board(int width, int height)
     {
          tiles = new Tile[width, height];
          dimensions = new Vector2Int(width, height);
     }
     
     public Tile getTile(Vector2Int coordinates)
     {
          return tiles[coordinates.x, coordinates.y];
     }

     public bool isInBoundsAndIsTargettable(Vector2Int coordinates)
     {
          if (coordinates.x > dimensions.x-1 || coordinates.x < 0) return false;
          if (coordinates.y > dimensions.y-1 || coordinates.y < 0) return false;
          
          if(!tiles[coordinates.x, coordinates.y].tileDefinition.isTargetable) return false;
          
          return true;
     }
     
     
}