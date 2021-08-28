using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScheduleSelectUI : MonoSingleton<ScheduleSelectUI>
{
    [SerializeField]
    private CalendarSlot _calendarSlot;
    [SerializeField]
    private Transform _scheduleButtonRoot;
    [SerializeField]
    private ScheduleButtonEntry _scheduleButtonPrefab;
    private ScheduleType[] _selectedSchedules = new ScheduleType[Constant.WEEK_PER_MONTH_COUNT];
    private int selectedScheduleCount = 0;

    private void Awake()
    {
        var scheduleDatas = GameData.I.Schedule.SelectableDatas;
        foreach (var data in scheduleDatas)
        {
            var scheduleButtonEntry = Instantiate(_scheduleButtonPrefab, _scheduleButtonRoot);
            scheduleButtonEntry.SetButton(data);
        }

        UpdateCalendar();
    }

    public void SelectSchedule(ScheduleType schedule)
    {
        if (selectedScheduleCount < _selectedSchedules.Length)
        {
            _selectedSchedules[selectedScheduleCount] = schedule;
            selectedScheduleCount += 1;
        }

        UpdateCalendar();
    }

    public void ResetSchedule()
    {
        for (int i = 0; i < _selectedSchedules.Length; i++)
        {
            _selectedSchedules[i] = ScheduleType.Invalid;
        }

        selectedScheduleCount = 0;

        UpdateCalendar();
    }

    private void UpdateCalendar()
    {
        _calendarSlot.SetCalender(_selectedSchedules);
    }
}
