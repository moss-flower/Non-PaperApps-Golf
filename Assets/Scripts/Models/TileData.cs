
namespace Models
{
    /// <summary>
    /// Bass class representing tile data at a simple level.
    /// </summary>
    /// <remarks>I actually don't know why this exists. Presumably for the editor?</remarks>
    [System.Serializable]
    public class TileData
    {
        public int x;
        public int y;
        public string type;
    }
}