using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationTestSetting : MonoBehaviour
{
    [Header("골드")]
    public int Gold;
    [Header("체력")]
    public int Stamina;
    [Header("심폐지구력")]
    public int Endurance;
    [Header("순발력")]
    public int Quickness;
    [Header("근력")]
    public int Strength;
    [Header("유연성")]
    public int Flexibility;

    private void Awake()
    {
        var stats = new Dictionary<StatType, int>();
        stats.Add(StatType.Stamina, Stamina);
        stats.Add(StatType.Endurance, Endurance);
        stats.Add(StatType.Quickness, Quickness);
        stats.Add(StatType.Strength, Strength);
        stats.Add(StatType.Flexibility, Flexibility);
        Simulation.I.SetBaseStat(stats);
        Simulation.I.AddGold(Gold);
    }
}
