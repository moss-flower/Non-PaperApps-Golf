namespace Helpers
{
    /// <summary>
    /// Tool for converting player scores into golf terms for display.
    /// </summary>
    public class GolfScorer
    {
        public static string Score(int scoreDiff)
        {
            var scoreName = scoreDiff switch
            {
                0 => "Par",
                -1 => "Birdie",
                -2 => "Eagle",
                -3 => "Albatross",
                -4 => "Condor",
                -5 => "Ostrich",
                <= -6 => "Wait how did you...",
                1 => "Bogey",
                2 => "Double Bogey",
                3 => "Triple Bogey",
                4 => "Quadruple Bogey",
                >= 5 => "Bogeyman",
            };
            return scoreName;
        }
    }
}