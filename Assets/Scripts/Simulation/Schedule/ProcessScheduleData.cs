using System;
using System.Collections.Generic;

public class StatChangedInfo
{
    public StatType StatType;
    public int DiffValue;
    public int Value => Math.Abs(DiffValue);
    public bool IsIncreased => DiffValue > 0;
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

