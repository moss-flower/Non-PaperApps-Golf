using UnityEngine;

namespace Models
{
    public class PaintableTile : MonoBehaviour
    {
        public TileDefinition tileDefinition;

        public void ApplyDefinition(TileDefinition def)
        {
            tileDefinition = def;
        
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer == null)
            {
                return;
            }
            spriteRenderer.sprite = tileDefinition.sprite;
        }
    }
}