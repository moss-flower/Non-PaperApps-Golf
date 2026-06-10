using System;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public TileDefinition tileDefinition { get; private set; }
    public SpriteRenderer spriteRenderer { get; private set; }
    
    public void Initialize(TileDefinition tileDefinition)
    {
        this.tileDefinition = tileDefinition;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = tileDefinition.sprite;
    }

}
