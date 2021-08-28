using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayEntry : MonoBehaviour
{
    [SerializeField]
    private Image _scheduleIcon;

    [SerializeField]
    private Text _dayText;

    public void SetDayText(int day)
    {
        _dayText.text = day.ToString();
    }

    public void SetScheduleIcon(Sprite sprite)
    {
        _scheduleIcon.gameObject.SetActive(true);
        _scheduleIcon.sprite = sprite;
    }

    public void HideScheduleIcon()
    {
        _scheduleIcon.gameObject.SetActive(false);
    }
}