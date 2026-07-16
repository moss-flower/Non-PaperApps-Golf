using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BoardData
{
    public string name;
    public int width;
    public int height;
    public List<TileData> tiles;
    public Vector2Int winTileLocation;
    public Vector2Int startTileLocation;
    public int par;
}