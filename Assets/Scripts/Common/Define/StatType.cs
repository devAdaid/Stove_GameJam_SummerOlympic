using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatType
{
    Invalid = 0,

    // 체력
    Stamina,
    // 심폐지구력
    Endurance,
    // 순발력
    Quickness,
    // 근력
    Strength,
    // 유연성
    Flexibility,
}

internal static class StatExtend
{
    public static string GetString(this StatType statType) =>
    statType switch
    {
        StatType.Stamina => "체력",
        StatType.Endurance => "심폐지구력",
        StatType.Quickness => "순발력",
        StatType.Strength => "근력",
        StatType.Flexibility => "유연성",
        _ => ""
    };
}