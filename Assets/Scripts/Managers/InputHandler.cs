using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private CameraController cam;

    private InputAction panAction;
    private InputAction zoomAction;

    private void Awake()
    {
        panAction = InputSystem.actions.FindAction("Pan");
        zoomAction = InputSystem.actions.FindAction("Zoom");
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
    }
}
