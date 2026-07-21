using Managers;
using UnityEngine;

namespace Controller
{
    /// <summary>
    /// A class for handling user interaction with the game board.
    /// </summary>
    public class GameEventHandler : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        [SerializeField] private InputHandler inputHandler;

        
        private void OnEnable()
        {
            inputHandler.OnPause += TogglePause;
        }

        private void OnDisable()
        {
            inputHandler.OnPause -= TogglePause;
        }

        /// <summary>
        /// A method called when a clickable tile is selected by the player.
        /// </summary>
        /// <param name="x">The X coordinate of the tile on the game board.</param>
        /// <param name="y">The Y coordinate of the tile on the game board.</param>
        public void OnClickEvent(int x, int y)
        {
            gameManager.MoveEvent(x,y);
        }

        /// <summary>
        /// A method called when the role button is clicked.
        /// </summary>
        /// <remarks>Technically unnecessary but nice to condense the logic to a single location.</remarks>
        public void OnClickRole()
        {
            gameManager.HandleRoll();
        }
    
        /// <summary>
        /// A method called when the user presses the key mapped to pause.
        /// </summary>
        public void TogglePause()
        {
            gameManager.TogglePause();
        }
    }
}