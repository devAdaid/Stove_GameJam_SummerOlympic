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

    [Header("Swim")]
    public float defaultSwimmingSpeed;

    [Header("���� �ӵ� ���� �ӵ� (������)")]
    [SerializeField] float a_endurance;
    [SerializeField] float defaultEndurance;
    [Header("�῵ ���ӽð� (���߷�)")]
    [SerializeField] float a_quickness;
    [SerializeField] float defaultQuickness;
    [Header("�ִ� �ӵ� (�ٷ�)")]
    [SerializeField] float a_strength;
    [SerializeField] float defaultStrength;
    [Header("��ư ���� �� ���� �ӵ� (������)")]
    [SerializeField] float a_flexibility;
    [SerializeField] float defaultFlexibility;


    float endurance = 0; //�ִ� �ӵ� ���� ���ɽð� ( swimSpeed �����Ǵ� �ð����� �ϸ� �ɵ�)
    float quickness = 0; //��ŸƮ �῵ ���ӽð� ( diveSwimduration�� +)
    float strength = 0; //�ִ� �ӵ� (maxSpeed
    float flexibility = 0; //���ӵ� (bonusSwimmingSpeed�� +)

    List<int[]> tapSpeeds = new List<int[]>();

    public void SetStats(AthleteFSM [] athletes, int playerIndex)
    {
        //���� index ����, ������ ����
        List<AIStatData> statDatas = new List<AIStatData>();
        for(int i=0; i< athletes.Length - 1; i++)
        {
            statDatas.Add(GameData.I.AIStat.Datas[i]);
        }
        bool isPlayerSet = false;
        for(int i=0; i<athletes.Length; i++)
        {
            if(i == playerIndex)
            {
                athletes[i].name = Simulation.I.Swimmer.Name;
                athletes[i].swimSpeedLerpSpeed = a_endurance * Simulation.I.Swimmer.GetStat(StatType.Endurance) + defaultEndurance;
                athletes[i].diveSwimDuration = a_quickness * Simulation.I.Swimmer.GetStat(StatType.Quickness) + defaultQuickness;
                athletes[i].maxSwimmingSpeed = a_strength * Simulation.I.Swimmer.GetStat(StatType.Strength) + defaultStrength;
                athletes[i].bonusSwimmingSpeed = a_flexibility * Simulation.I.Swimmer.GetStat(StatType.Flexibility) + defaultFlexibility;
                tapSpeeds.Add(new int[4] { 0, 0, 0, 0 });
                isPlayerSet = true;
                continue;
            }
            AIStatData data = statDatas[i - (isPlayerSet ? 1 : 0)];
            athletes[i].name = data.Name;
            athletes[i].swimSpeedLerpSpeed = a_endurance * data.Endurance + defaultEndurance;
            athletes[i].diveSwimDuration = a_quickness * data.Quickness + defaultQuickness;
            athletes[i].maxSwimmingSpeed = a_strength * data.Strength + defaultStrength;
            athletes[i].bonusSwimmingSpeed = a_flexibility * data.Flexibility + defaultFlexibility;
            tapSpeeds.Add(data.TapSpeeds);
        }
    }
}