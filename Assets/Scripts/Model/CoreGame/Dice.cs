using System;

public class Dice
{
    private int _sides;
    private Random _random;
    public int Throws { get; private set; }
    public int Result { get; private set; }
    public bool CanSpawn { get { return Result == GameConstants.DICE_RESULT_FOR_SPAWNING; } }

    public Dice(int sides)
    {
        _random = new Random();
        _sides = sides;
        Reset();
    }

    public void Roll()
    {
        Result = _random.Next(1, _sides + 1);
        Throws++;
    }

    public void Reset()
    {
        Throws = 0;
    }
}