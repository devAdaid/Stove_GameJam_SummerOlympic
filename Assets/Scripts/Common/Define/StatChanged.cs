using System.Collections.Generic;
using UnityEngine;

public class StatChangedInfo
{
    public StatType StatType;
    public int DiffValue;
    public int Value => Mathf.Abs(DiffValue);
    public bool IsIncreased => DiffValue > 0;
}

public class 진주님_클래스
{
    public void ProcessSchedule(List<StatChangedInfo> statChangedInfo, string scheduleName)
    {
        // 일정을 증가시키고 타이핑 연출을 보여줌....
    }
}