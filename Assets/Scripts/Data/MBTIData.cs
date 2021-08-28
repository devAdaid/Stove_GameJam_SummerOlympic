using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MBTIData
{
    public readonly string MBTI;
    public readonly int Endurance;
    public readonly int Quickness;
    public readonly int Strength;
    public readonly int Flexibility;

    public MBTIData(
        string mbti,
        int endurance, int quickness,
        int strength, int flexibility)
    {
        MBTI = mbti;
        Endurance = endurance;
        Quickness = quickness;
        Strength = strength;
        Flexibility = flexibility;
    }
}
