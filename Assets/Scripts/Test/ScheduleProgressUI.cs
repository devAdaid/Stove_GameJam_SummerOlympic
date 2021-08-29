using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//일정 진행 테스트 코드입니다.
public class ScheduleProgressUI : MonoBehaviour
{
    [Header("Stat Names Text")]
    public Text staminaText;
    public Text strengthText;
    public Text quicknessText;
    public Text enduranceText;
    public Text flexibilityText;

    [Header("Stat Values Text")]
    public Text staminaValue;
    public Text strengthValue;
    public Text quicknessValue;
    public Text enduranceValue;
    public Text flexibilityValue;

    [Header("Stat Sliders")]
    public Slider staminaSlider;
    public Slider strengthSlider;
    public Slider quicknessSlider;
    public Slider enduranceSlider;
    public Slider flexibilitySlider;

    [Space(10f)]
    public Text currentGold;

    void Start()
    {
        UpdateStatName();
        UpdateStatValue();
        UpdateGoldUI();
    }

    public void AddStemina()
    {
        // 스탯을 1 (일정 값만큼) 증가시키는 예제.
        Simulation.I.IncreaseSwimmerStat(StatType.Stamina, 1);

        UpdateStatValue();
        UpdateGoldUI();
    }

    public void AddStrength()
    {
        // 스탯을 1 (일정 값만큼) 증가시키는 예제.
        Simulation.I.IncreaseSwimmerStat(StatType.Strength, 1);

        UpdateStatValue();
        UpdateGoldUI();
    }

    public void AddQuickness()
    {
        // 스탯을 1 (일정 값만큼) 증가시키는 예제.
        Simulation.I.IncreaseSwimmerStat(StatType.Quickness, 1);

        UpdateStatValue();
        UpdateGoldUI();
    }

    public void AddEndurance()
    {
        // 스탯을 1 (일정 값만큼) 증가시키는 예제.
        Simulation.I.IncreaseSwimmerStat(StatType.Endurance, 1);

        UpdateStatValue();
        UpdateGoldUI();
    }

    public void AddFlexibility()
    {
        // 스탯을 1 (일정 값만큼) 증가시키는 예제.
        Simulation.I.IncreaseSwimmerStat(StatType.Flexibility, 1);

        UpdateStatValue();
        UpdateGoldUI();
    }

    private void UpdateStatName()
    {
        staminaText.text = GetStatName(StatType.Stamina);
        enduranceText.text = GetStatName(StatType.Endurance);
        quicknessText.text = GetStatName(StatType.Quickness);
        strengthText.text = GetStatName(StatType.Strength);
        flexibilityText.text = GetStatName(StatType.Flexibility);
    }

    private void UpdateStatValue()
    {
        var sta = GetStatValue(StatType.Stamina);
        var end = GetStatValue(StatType.Endurance);
        var qui = GetStatValue(StatType.Quickness);
        var str = GetStatValue(StatType.Strength);
        var fle = GetStatValue(StatType.Flexibility);

        staminaValue.text = sta;
        enduranceValue.text = end;
        quicknessValue.text = qui;
        strengthValue.text = str;
        flexibilityValue.text = fle;

        staminaSlider.value = float.Parse(sta);
        enduranceSlider.value = float.Parse(end);
        quicknessSlider.value = float.Parse(qui);
        strengthSlider.value = float.Parse(str);
        flexibilitySlider.value = float.Parse(fle);
    }

    private void UpdateGoldUI()
    {
        currentGold.text = GetGold() + "원";
    }

    private string GetStatName(StatType statType)
    {
        // StatType.GetString()으로 스탯의 한글 이름을 가져올 수 있다.
        return $"{statType.GetString()}";
    }

    private string GetStatValue(StatType statType)
    {
        // 언제어디서나 Simulation.I.Swimmer.GetStat()으로 수영선수의 스탯 값을 가져올 수 있다.
        return $"{Simulation.I.Swimmer.GetStat(statType)}";
    }

    private string GetGold()
    {
        //수영선수의 골드를 가져옴
        return $"{Simulation.I.Gold}";
    }
}
