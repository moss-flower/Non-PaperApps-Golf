using System;
using UnityEngine;

public class Tile : MonoBehaviour, IClickable
{
    public TileDefinition tileDefinition { get; private set; }
    public SpriteRenderer spriteRenderer { get; private set; }
    
    public Vector2Int coordinates { get; private set; }
    
    private bool isClickable = false;
    
    public void Initialize(TileDefinition tileDefinition, int x, int y)
    {
        this.tileDefinition = tileDefinition;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = tileDefinition.sprite;
        this.coordinates = new Vector2Int(x, y);
    }

    public void OnClicked()
    {
        if (isClickable)
        {
            EventManager.instance.OnClickEvent(coordinates.x, coordinates.y);
        }
        
    }
    
    public void makeClickable()
    {
        isClickable = true;
        spriteRenderer.color = Color.deepPink;
    }
    public void makeUnclickable()
    {
        isClickable = false;
        spriteRenderer.color = Color.white;
    }


}
