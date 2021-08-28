using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimmerCharacter
{
    public string Name { get; private set; }
    private Dictionary<StatType, int> _statTable = new Dictionary<StatType, int>();

    public void SetName(string name)
    {
        Name = name;
    }

    public void SetBaseStat(Dictionary<StatType, int> stats)
    {
        _statTable = stats;
    }

    public int GetStat(StatType statType)
    {
        if (_statTable.TryGetValue(statType, out var value))
        {
            return value;
        }

        return 0;
    }

    public void IncreaseStat(StatType statType, int value)
    {
        if (_statTable.TryGetValue(statType, out var previousValue))
        {
            _statTable[statType] = previousValue + value;
            return;
        }

        _statTable.Add(statType, value);
    }

    public void DecreaseStat(StatType statType, int value)
    {
        if (_statTable.TryGetValue(statType, out var previousValue))
        {
            _statTable[statType] = previousValue - value;
            return;
        }
    }
}
