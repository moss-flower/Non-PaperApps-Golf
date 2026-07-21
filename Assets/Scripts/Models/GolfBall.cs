using UnityEngine;

namespace Models
{
    public class GolfBall : MonoBehaviour
    {
        [SerializeField] private Sprite sprite;
        public int modifier {get; set;}
        public bool canHopWalls {get; set;}
        public bool canPutt {get; set;}

        public Vector2Int boardPosition { get; private set; }

        private void Awake()
        {
            modifier = 0;
            canHopWalls = false;
            canPutt = false;
            boardPosition = new Vector2Int(0, 0);
        }

        public void move(Vector3 direction, Vector2Int position)
        {
            transform.position = direction;
            boardPosition = position;
            print(boardPosition);
        }

    
    }
}