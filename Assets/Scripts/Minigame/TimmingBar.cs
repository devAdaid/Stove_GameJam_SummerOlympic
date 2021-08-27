using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TimmingBar : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Slider slider;

    [Header("Value Settings")]
    [SerializeField] float decreasingSpeed;

    bool isTimerStopped;

    public void StartTimer()
    {
        StartCoroutine(StartTimerCoroutine());
    }
    public float StopAndGetTimming()
    {
        isTimerStopped = true;
        return slider.value;
    }
    private void Awake()
    {
        slider.maxValue = 100;
        slider.value = 100;
        isTimerStopped = false;
    }
    IEnumerator StartTimerCoroutine()
    {
        while(!isTimerStopped)
        {
            slider.value -= decreasingSpeed * Time.deltaTime;
            yield return null;
        }
    }

}
