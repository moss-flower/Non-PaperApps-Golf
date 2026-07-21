
namespace Models
{
    /// <summary>
    /// Represents the currently active game State, minus the current player position (controller by the ball).
    /// </summary>
    public class GameState
    {
        private string PlayerName { get; set; } = "Player";
        private int Score { get; set; } = 0;
        public int RemainingMulligans { get; private set; } = 3;

        private bool hasStarted = false;
        private bool hasRolled = false;

        public void startGame()
        {
            reset();
            hasStarted = true;
        }

        public void endGame()
        {
            hasStarted = false;
        }

        public bool HasStarted()
        {
            return hasStarted;
        }
    
        public void reset()
        {
            Score = 0;
            RemainingMulligans = 3;
            hasRolled = false;
        }

        public void incrementScore()
        {
            Score++;
            hasRolled = false;
        }

        public void decrementScore()
        {
            Score--;
        }

        public void incrementMulligans()
        {
            RemainingMulligans++;
        }
    
        public void decrementMulligans()
        {
            RemainingMulligans--;
        }

        public void setHasRolled(bool hasRolled)
        {
            this.hasRolled = hasRolled;
        }

        public bool HasRolled()
        {
            return  hasRolled;
        }

        public int getScore()
        {
            return Score;
        }
    }
}
