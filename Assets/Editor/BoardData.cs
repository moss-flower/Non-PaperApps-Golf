using System.Collections.Generic;

[System.Serializable]
public class BoardData
{
    public string name;
    public int width;
    public int height;
    public List<TileData> tiles;
}