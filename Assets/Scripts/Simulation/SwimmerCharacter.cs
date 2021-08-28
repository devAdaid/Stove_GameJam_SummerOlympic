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
            var afterValue = previousValue + value;
            var maxValue = (statType == StatType.Stamina) ? Constant.STAMINA_MAX : Constant.SWIMSTAT_MAX;
            if (afterValue > maxValue) afterValue = maxValue;

            _statTable[statType] = afterValue;
            return;
        }

        _statTable.Add(statType, value);
    }

    public void DecreaseStat(StatType statType, int value)
    {
        if (_statTable.TryGetValue(statType, out var previousValue))
        {
            var afterValue = previousValue - value;
            if (afterValue < 0) afterValue = 0;

            _statTable[statType] = afterValue;
            return;
        }
    }
}
