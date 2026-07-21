using Controller;
using Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

namespace Models
{
    public class Tile : MonoBehaviour, IClickable
    {
        [FormerlySerializedAs("eventManager")] [SerializeField] private GameEventHandler gameEventHandler;
        public TileDefinition tileDefinition { get; private set; }
        public SpriteRenderer spriteRenderer { get; private set; }
    
        public Vector2Int coordinates { get; private set; }
    
        private bool isClickable = false;
    
        public void Initialize(TileDefinition tileDefinition, int x, int y, GameEventHandler gameEventHandler)
        {
            this.gameEventHandler = gameEventHandler;
            this.tileDefinition = tileDefinition;
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = tileDefinition.sprite;
            this.coordinates = new Vector2Int(x, y);
        }

        public void OnClicked()
        {
            if (isClickable)
            {
                gameEventHandler.OnClickEvent(coordinates.x, coordinates.y);
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
}
