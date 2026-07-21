using Controller;
using Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

namespace Models
{
    /// <summary>
    /// A game object representing an in game tile, a position to which the ball can move.
    /// </summary>
    public class Tile : MonoBehaviour, IClickable
    {
        [FormerlySerializedAs("eventManager")] [SerializeField] private GameEventHandler gameEventHandler;
        public TileDefinition tileDefinition { get; private set; }
        public SpriteRenderer spriteRenderer { get; private set; }
    
        public Vector2Int coordinates { get; private set; }
    
        private bool isClickable = false;
        
        
        /// <summary>
        /// A method used to construct tile info after the instantiation of the game object.
        /// </summary>
        /// <param name="tileDefinition">The characteristics of the tile, expressed as a Tile Definition object</param>
        /// <param name="x">The horizontal position of the tile in the game board</param>
        /// <param name="y">The vertical position of the tile in the game board</param>
        /// <param name="gameEventHandler">A reference to the object that handles user interaction.</param>
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
