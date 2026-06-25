public class GameState
{
    private string PlayerName { get; set; } = "Player";
    private int Score { get; set; } = 0;
    private int RemainingMulligans { get; set; } = 3;

    public void reset()
    {
        Score = 0;
        RemainingMulligans = 3;
    }

    public void incrementScore()
    {
        Score++;
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
}