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
    public float diveSwimDuration;
    public float bonusDiveSwimDuration;
    public float diveSwimSpeed;

    [Header("Dive Recover")]
    public float recoverDuration;

    [Header("Swim")]
    public float defaultSwimmingSpeed;
    public float bonusSwimmingSpeed;

    float endurance=0; //�ִ� �ӵ� ���� ���ɽð� ( swimSpeed �����Ǵ� �ð����� �ϸ� �ɵ�)
    float quickness=0; //��ŸƮ �῵ ���ӽð� ( diveSwimduration�� +)
    float strength=0; //�ִ� �ӵ� (maxSpeed
    float flexibility=0; //���ӵ� (bonusSwimmingSpeed�� +)

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

    public void SetStats(AthleteFSM [] athletes, int playerIndex)
    {
        for(int i=0; i<athletes.Length; i++)
        {
            athletes[i].swimSpeedLerpSpeed = a_endurance * endurance + defaultEndurance;
            athletes[i].diveSwimDuration = a_quickness * quickness + defaultQuickness;
            athletes[i].maxSwimmingSpeed = a_strength * strength + defaultStrength;
            athletes[i].bonusSwimmingSpeed = a_flexibility * flexibility + defaultFlexibility;
        }
    }
}
