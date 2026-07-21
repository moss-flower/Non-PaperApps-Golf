using System;
using Controller;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Managers
{
    /// <summary>
    /// Handles conversion of user input into game actions.
    /// </summary>
    public class InputHandler : MonoBehaviour
    {
        [SerializeField] private CameraController cam;

        private InputAction panAction;
        private InputAction zoomAction;
        private InputAction pauseAction;

        public event Action OnPause;

        
        private void Awake()
        {
            panAction = InputSystem.actions.FindAction("Pan");
            zoomAction = InputSystem.actions.FindAction("Zoom");
            pauseAction = InputSystem.actions.FindAction("Escape");
        }

        private void Update()
        {
            Vector2 panValue =  panAction.ReadValue<Vector2>();
            if (panValue.x != 0 || panValue.y != 0)
            {
                cam.Pan(panValue);
            }
        
            float zoomValue =  zoomAction.ReadValue<float>();
            if (zoomValue != 0)
            {
                cam.Zoom(zoomValue);
            }

            if (pauseAction.triggered)
            {
                OnPause?.Invoke();
            }
        }
    }
}
