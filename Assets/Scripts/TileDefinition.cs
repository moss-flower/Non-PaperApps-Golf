using UnityEngine;

[CreateAssetMenu(fileName = "NewTileDefinition", menuName = "Scripts/TileDefinition")]
public class TileDefinition : ScriptableObject
{
    public string tileName;
    public int modifier;
    public bool isTall;
    public bool isTargetable;
    public bool isPuttable;
    public Sprite sprite;
}
