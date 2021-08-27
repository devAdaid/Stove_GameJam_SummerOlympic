using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 시뮬레이션 기반 코드들을 사용하는 예제 코드 입니다.
/// </summary>
public class SimulationTest : MonoBehaviour
{
    public Text staminaText;
    public Text enduranceText;
    public Text quicknessText;
    public Text strengthText;
    public Text flexibilityText;

    void Awake()
    {
        // MBTI 등으로 정해진 기본 스탯을 적용한다.
        // 여기서는 테스트를 위해 하드코딩된 스탯을 적용한다.
        var baseStat = new Dictionary<StatType, int>()
        {
            {StatType.Stamina, 5},
            {StatType.Endurance, 3},
            {StatType.Quickness, 4},
            {StatType.Strength, 2},
            {StatType.Flexibility, 1}
        };

        Simulation.I.SetBaseStat(baseStat);

        UpdateStatUI();
    }

    public void AddStemina()
    {
        // 스탯을 1 (일정 값만큼) 증가시키는 예제.
        Simulation.I.IncreaseSwimmerStat(StatType.Stamina, 1);

        UpdateStatUI();
    }

    private void UpdateStatUI()
    {
        staminaText.text = GetStatText(StatType.Stamina);
        enduranceText.text = GetStatText(StatType.Endurance);
        quicknessText.text = GetStatText(StatType.Quickness);
        strengthText.text = GetStatText(StatType.Strength);
        flexibilityText.text = GetStatText(StatType.Flexibility);
    }

    private string GetStatText(StatType statType)
    {
        // StatType.GetString()으로 스탯의 한글 이름을 가져올 수 있다.
        // 언제어디서나 Simulation.I.Swimmer.GetStat()으로 수영선수의 스탯 값을 가져올 수 있다.
        return $"[{statType.GetString()}] {Simulation.I.Swimmer.GetStat(statType)}";
    }
}
