namespace Helpers
{
    public class GolfScorer
    {
        public string Score(int score_diff)
        {
            string score_name = score_diff switch
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
            return score_name;
        }
    }
}