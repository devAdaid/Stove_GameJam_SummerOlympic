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

    float endurance=0; //최대 속도 유지 가능시간 ( swimSpeed 복구되는 시간으로 하면 될듯)
    float quickness=0; //스타트 잠영 지속시간 ( diveSwimduration에 +)
    float strength=0; //최대 속도 (maxSpeed
    float flexibility=0; //가속도 (bonusSwimmingSpeed에 +)

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
