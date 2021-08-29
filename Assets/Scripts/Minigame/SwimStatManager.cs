using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimStatManager : MonoBehaviour
{
    [Header("Dive")]
    public float diveDuration;
    public float diveSpeed;
    public float diveDepth;

    [Header("Dive Timming")]
    public float[] diveTimmingBounds;
    public float[] diveSwimDurationAlphas;

    [Header("Dive Swim")]
    public float bonusDiveSwimDuration;
    public float diveSwimSpeed;

    [Header("Dive Recover")]
    public float recoverDuration;


    [Header("수영 속도 감소 속도 (지구력)")]
    [SerializeField] float a_endurance;
    [SerializeField] float defaultEndurance;
    [Header("잠영 지속시간 (순발력)")]
    [SerializeField] float a_quickness;
    [SerializeField] float defaultQuickness;
    [Header("최대 속도 (근력)")]
    [SerializeField] float a_strength;
    [SerializeField] float defaultStrength;
    [Header("버튼 누를 때 증가 속도 (유연성)")]
    [SerializeField] float a_flexibility;
    [SerializeField] float defaultFlexibility;

    [Header("테스트용")]
    [SerializeField] bool test = false;
    [SerializeField] float endurance = 0; //최대 속도 유지 가능시간 ( swimSpeed 복구되는 시간으로 하면 될듯)
    [SerializeField] float quickness = 0; //스타트 잠영 지속시간 ( diveSwimduration에 +)
    [SerializeField] float strength = 0; //최대 속도 (maxSpeed
    [SerializeField] float flexibility = 0; //가속도 (bonusSwimmingSpeed에 +)
    [SerializeField] int[] otherSwimmerIndicies;

    public void SetStats(AthleteFSM[] athletes, int playerIndex)
    {
        //선수 index 결정, 데이터 보관
        List<AIStatData> statDatas = new List<AIStatData>();
        for (int i = 0; i < athletes.Length - 1; i++)
        {
            if (test)

                statDatas.Add(GameData.I.AIStat.Datas[otherSwimmerIndicies[i]]);
            else
                statDatas.Add(GameData.I.AIStat.Datas[SwimGameManager.SwimmerIndicies[i]]);
        }
        bool isPlayerSet = false;
        for (int i = 0; i < athletes.Length; i++)
        {
            if (i == playerIndex)
            {
                athletes[i].name = Simulation.I.Swimmer.Name;
                if (test)
                {

                    athletes[i].swimSpeedLerpSpeed = a_endurance * endurance + defaultEndurance;
                    athletes[i].diveSwimDuration = a_quickness * quickness + defaultQuickness;
                    athletes[i].maxSwimmingSpeed = a_strength * strength + defaultStrength;
                    athletes[i].bonusSwimmingSpeed = a_flexibility * flexibility + defaultFlexibility;


                }
                else
                {

                    athletes[i].swimSpeedLerpSpeed = a_endurance * Simulation.I.Swimmer.GetStat(StatType.Endurance) + defaultEndurance;
                    athletes[i].diveSwimDuration = a_quickness * Simulation.I.Swimmer.GetStat(StatType.Quickness) + defaultQuickness;
                    athletes[i].maxSwimmingSpeed = a_strength * Simulation.I.Swimmer.GetStat(StatType.Strength) + defaultStrength;
                    athletes[i].bonusSwimmingSpeed = a_flexibility * Simulation.I.Swimmer.GetStat(StatType.Flexibility) + defaultFlexibility;

                }
                athletes[i].defaultSwimmingSpeed = athletes[i].maxSwimmingSpeed * 0.5f;
                athletes[i].flagType = 0;
                athletes[i].tapSpeeds = new int[4] { 0, 0, 0, 0 };
                athletes[i].diveStat = 0;
                isPlayerSet = true;
                continue;
            }
            AIStatData data = statDatas[i - (isPlayerSet ? 1 : 0)];
            athletes[i].name = data.Name;
            athletes[i].swimSpeedLerpSpeed = a_endurance * data.Endurance + defaultEndurance;
            athletes[i].diveSwimDuration = a_quickness * data.Quickness + defaultQuickness;
            athletes[i].maxSwimmingSpeed = a_strength * data.Strength + defaultStrength;
            athletes[i].defaultSwimmingSpeed = athletes[i].maxSwimmingSpeed * 0.5f;
            athletes[i].bonusSwimmingSpeed = a_flexibility * data.Flexibility + defaultFlexibility;
            athletes[i].flagType = data.FlagType;
            athletes[i].tapSpeeds = data.TapSpeeds;
            athletes[i].diveStat = data.DiveStat;
        }
    }
}
