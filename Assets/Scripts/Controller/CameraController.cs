using UnityEngine;

namespace Controller
{
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

        public void Pan(Vector2 dir)
        {
            Vector3 movement = Vector3.ClampMagnitude(dir, 1f);
            transform.position += Time.deltaTime * cam_pan_speed * movement;
        }

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
