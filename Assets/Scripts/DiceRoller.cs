using Random = System.Random;

public class DiceRoller
{
    public DiceRoller()
    {
        LastRoll = 0;
    }
    
    public int LastRoll {get; private set;}
    
    public int Roll()
    {
        Random random = new Random();
        int roll = random.Next(1);
        LastRoll = roll;
        return roll;
    }
}
