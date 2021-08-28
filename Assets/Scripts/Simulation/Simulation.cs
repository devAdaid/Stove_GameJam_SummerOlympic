using System.Collections.Generic;

public class Simulation : Singleton<Simulation>
{
    public SwimmerCharacter Swimmer { get; private set; }
    public int Gold { get; private set; }

    public Simulation()
    {
        Swimmer = new SwimmerCharacter();
    }

    public void Reset()
    {
        Swimmer = new SwimmerCharacter();
        Gold = 0;
    }

    public void SetBaseStat(Dictionary<StatType, int> stats)
    {
        Swimmer.SetBaseStat(stats);
    }

    public void IncreaseSwimmerStat(StatType statType, int value)
    {
        Swimmer.IncreaseStat(statType, value);
    }

    public void AddGold(int value)
    {
        Gold += value;
    }

    public bool CanPayGold(int value)
    {
        return Gold >= value;
    }

    public bool DecreaseGold(int value)
    {
        if (!CanPayGold(value))
        {
            return false;
        }

        Gold -= value;
        return true;
    }
}