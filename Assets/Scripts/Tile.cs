using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector2Int coordinates { get; private set; }
    public TileDefinition tileDefinition { get; private set; }

    void Initialize(Vector2Int coordinates, TileDefinition tileDefinition)
    {
        this.coordinates = coordinates;
        this.tileDefinition = tileDefinition;
    }
}
