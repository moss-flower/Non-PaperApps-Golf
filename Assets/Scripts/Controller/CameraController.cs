using UnityEngine;

namespace Controller
{
    /// <summary>
    /// Basic optional class that allows a user to change the position of the game camera.
    /// </summary>
    public class CameraController : MonoBehaviour
    {
        private Camera camera;
        private Transform cam;
        private Vector3 cam_pos;
        private Vector3 cam_rot;
        private Vector3 cam_scale;
        private Vector3 cam_offset;

        private float camSize;

        [SerializeField] private float cam_pan_speed;
        [SerializeField] private float cam_zoom_speed;

        
        
        private void OnEnable()
        {
            camera = Camera.main;
            cam = this.transform;
            cam_pos = cam.position;
        }

        /// <summary>
        /// Method for panning the camera.
        /// </summary>
        /// <param name="dir">Takes a two-dimensional vector corresponding to the x and y coordinates
        /// passed through by the player.</param>
        public void Pan(Vector2 dir)
        {
            Vector3 movement = Vector3.ClampMagnitude(dir, 1f);
            transform.position += Time.deltaTime * cam_pan_speed * movement;
        }

        /// <summary>
        /// Method for changing the orthographic size of the camera (zoom).
        /// </summary>
        /// <param name="zoom">Simply a positive or negative float value.</param>
        public void Zoom(float zoom)
        {
            if (zoom > 0)
            {
                camera.orthographicSize -= Time.deltaTime * cam_zoom_speed * 10;
            }
            else
            {
                camera.orthographicSize += Time.deltaTime * cam_zoom_speed * 10;
            }
        }
    }
}
