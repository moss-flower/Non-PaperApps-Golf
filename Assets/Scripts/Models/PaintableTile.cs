using UnityEngine;

namespace Models
{
    /// <summary>
    /// Class used as a base tile for the level creation Editor tool.
    /// </summary>
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