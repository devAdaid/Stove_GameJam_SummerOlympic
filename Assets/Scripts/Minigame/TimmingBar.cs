using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TimmingBar : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Slider slider;

    public float Value { get { return slider.value; } set { slider.value = value; } }


}
