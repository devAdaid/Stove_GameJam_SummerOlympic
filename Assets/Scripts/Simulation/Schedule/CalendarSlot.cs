using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalendarSlot : MonoBehaviour
{
    [SerializeField]
    private List<WeekEntry> _weekEntries;

    private void Start()
    {
        SetDayText();
    }

    public void SetCalender(ScheduleType[] selectedSchedules)
    {
        for (int i = 0; i < _weekEntries.Count; i++)
        {
            var scheduleType = selectedSchedules[i];
            if (scheduleType != ScheduleType.Invalid)
            {
                var iconSprite = GameData.I.Schedule.GetData(scheduleType).IconSprite;
                _weekEntries[i].SetScheduleIcon(iconSprite);
            }
            else
            {
                _weekEntries[i].HideScheduleIcon();
            }
        }
    }

    private void SetDayText()
    {
        for (int weekIndex = 0; weekIndex < _weekEntries.Count; ++weekIndex)
        {
            _weekEntries[weekIndex].SetDayText(weekIndex);
        }
    }
}
