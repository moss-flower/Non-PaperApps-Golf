using UnityEngine;

namespace Models
{
    /// <summary>
    /// A scriptable object representing all the characteristics a tile can have.
    /// New tiles are created by adding new combinations of these flags together.
    /// </summary>
    [CreateAssetMenu(fileName = "NewTileDefinition", menuName = "Scripts/TileDefinition")]
    public class TileDefinition : ScriptableObject
    {
        public string tileName;
        public int modifier;
        public bool isTall;
        public bool isTargetable;
        public bool isPuttable;
        public bool isClearView;
        public bool isStartingTile;
        public bool isWinningTile;
        public Sprite sprite;
    }
}
