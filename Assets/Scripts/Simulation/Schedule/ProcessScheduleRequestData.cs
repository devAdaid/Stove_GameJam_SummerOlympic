using System;
using System.Collections.Generic;

public class StatChangedInfo
{
    public readonly StatType StatType;
    public readonly int DiffValue;
    public int Value => Math.Abs(DiffValue);
    public bool IsIncreased => DiffValue > 0;

    public StatChangedInfo(StatType statType, int diffValue)
    {
        StatType = statType;
        DiffValue = diffValue;
    }
}

public class ProcessScheduleRequestData
{
    public readonly List<StatChangedInfo> StatChangedInfo;
    public readonly int GoldDiff;

    public ProcessScheduleRequestData(List<StatChangedInfo> statChangedInfo, int goldDiff)
    {
        StatChangedInfo = statChangedInfo;
        GoldDiff = goldDiff;
    }
}