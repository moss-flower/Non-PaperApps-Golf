using UnityEngine;

namespace Models
{
    /// <summary>
    /// Class that represents the visual portion and state of the golf ball object.
    /// </summary>
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

        /// <summary>
        /// Changes the balls position in the on the gameboard and in the actual "physical" game coordinates.
        /// </summary>
        /// <param name="direction">The game coordinates position the visual part of the ball is moved to,
        /// represented as a <see cref="Vector3"/></param>
        /// <param name="position">The 2d coordinates of the balls position on the game board.</param>
        public void move(Vector3 direction, Vector2Int position)
        {
            transform.position = direction;
            boardPosition = position;
            print(boardPosition);
        }

    
    }
}