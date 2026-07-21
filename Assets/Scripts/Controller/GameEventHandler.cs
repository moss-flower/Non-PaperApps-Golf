using Managers;
using UnityEngine;

namespace Controller
{
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

        public void OnClickEvent(int x, int y)
        {
            gameManager.MoveEvent(x,y);
        }

        public void OnClickRole()
        {
            gameManager.HandleRoll();
        }
    
        public void TogglePause()
        {
            gameManager.TogglePause();
        }
    }
}