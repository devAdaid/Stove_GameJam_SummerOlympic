using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeekEntry : MonoBehaviour
{
    [SerializeField]
    private List<DayEntry> _dayEntries;
    [SerializeField]
    private int _weekIndex;

    public void SetDayText(int weekIndex)
    {
        for (int dayIndex = 0; dayIndex < Constant.DAY_PER_WEEK_COUNT; dayIndex++)
        {
            var day = Constant.DAY_PER_WEEK_COUNT * weekIndex + 1 + dayIndex;
            _dayEntries[dayIndex].SetDayText(day);
        }
    }

    public void SetScheduleIcon(Sprite sprite)
    {
        foreach (var entry in _dayEntries)
        {
            entry.SetScheduleIcon(sprite);
        }
    }

    public void HideScheduleIcon()
    {
        foreach (var entry in _dayEntries)
        {
            entry.HideScheduleIcon();
        }
    }
}
