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
        var month = Simulation.I.GetMonth();

        int fixedWeek = -1;
        if (GameData.I.FixedSchedule.TryGetData(month, out var fixedScheduleData))
        {
            fixedWeek = fixedScheduleData.Week;
        }

        int s = 0;
        for (int i = 0; i < _weekEntries.Count; i++)
        {
            var scheduleType = selectedSchedules[s];
            if (scheduleType == ScheduleType.Match)
            {
                var iconSprite = GetFixedSchedule().IconSprite;
                _weekEntries[i].SetScheduleIcon(iconSprite);
            }
            else if (scheduleType != ScheduleType.Invalid)
            {
                var iconSprite = GameData.I.Schedule.GetData(scheduleType).IconSprite;
                _weekEntries[i].SetScheduleIcon(iconSprite);
            }
            else
            {
                _weekEntries[i].HideScheduleIcon();
            }
            s += 1;
        }
    }

    private void SetDayText()
    {
        for (int weekIndex = 0; weekIndex < _weekEntries.Count; ++weekIndex)
        {
            _weekEntries[weekIndex].SetDayText(weekIndex);
        }
    }

    private FixedScheduleData GetFixedSchedule()
    {
        var month = Simulation.I.GetMonth();
        if (GameData.I.FixedSchedule.TryGetData(month, out var data))
        {
            return data;
        }
        return null;
    }
}
