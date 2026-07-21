using Random = System.Random;

namespace Helpers
{
    /// <summary>
    /// Random number generator for handling in game rolls.
    /// </summary>
    /// <remarks>Should be expanded to allow for multiple types of dice (simple upper value range)</remarks>
    public class DiceRoller
    {
        public DiceRoller()
        {
            lastRoll = 0;
        }
    
        public int lastRoll {get; private set;}
    
        public int Roll()
        {
            Random random = new Random();
            int roll = random.Next(6) + 1;
            lastRoll = roll;
            return roll;
        }
    }
}


