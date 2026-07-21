using UnityEngine;

namespace Models
{
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
