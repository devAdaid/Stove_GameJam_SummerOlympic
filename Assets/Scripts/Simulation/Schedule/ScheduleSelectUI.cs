using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScheduleSelectUI : MonoSingleton<ScheduleSelectUI>
{
    [SerializeField]
    private Text _monthText;
    [SerializeField]
    private CalendarSlot _calendarSlot;
    [SerializeField]
    private SwimmerInfoSlot _swimmerInfo;
    [SerializeField]
    private CostPreviewSlot _costPreviewSlot;
    [SerializeField]
    private Transform _scheduleButtonRoot;
    [SerializeField]
    private ScheduleButtonEntry _scheduleButtonPrefab;
    [SerializeField]
    private ScheduleType[] _selectedSchedules = new ScheduleType[Constant.WEEK_PER_MONTH_COUNT];
    private int _currentScheduleIndex = 0;
    private List<ScheduleButtonEntry> _scheduleButtonEntries = new List<ScheduleButtonEntry>();
    private int _goldPreview => Simulation.I.Gold - GetTotalGoldCost();

    private void Awake()
    {
        var scheduleDatas = GameData.I.Schedule.SelectableDatas;
        foreach (var data in scheduleDatas)
        {
            var scheduleButtonEntry = Instantiate(_scheduleButtonPrefab, _scheduleButtonRoot);
            scheduleButtonEntry.SetButton(data, _goldPreview);
            _scheduleButtonEntries.Add(scheduleButtonEntry);
        }

        UpdateUI();
    }

    public void SelectSchedule(ScheduleType schedule)
    {
        if (Simulation.I.IsGameEnded)
        {
            return;
        }

        var scheduleData = GameData.I.Schedule.GetData(schedule);
        if (_goldPreview < scheduleData.GoldCost)
        {
            return;
        }

        if (_currentScheduleIndex < _selectedSchedules.Length)
        {
            _selectedSchedules[_currentScheduleIndex] = schedule;
            _currentScheduleIndex += 1;
            CheckFixedAndMoveIndex();
        }

        UpdateUI();
    }

    public void ResetSchedule()
    {
        for (int i = 0; i < _selectedSchedules.Length; i++)
        {
            _selectedSchedules[i] = ScheduleType.Invalid;
        }

        var month = Simulation.I.GetMonth();
        if (GameData.I.FixedSchedule.TryGetData(month, out var fixedScheduleData))
        {
            int weekIndex = fixedScheduleData.Week - 1;
            _selectedSchedules[weekIndex] = ScheduleType.Match;
        }

        _currentScheduleIndex = 0;
        CheckFixedAndMoveIndex();

        UpdateUI();
    }

    public void StartSchedule()
    {
        if (Simulation.I.IsGameEnded)
        {
            return;
        }

        if (_currentScheduleIndex < Constant.WEEK_PER_MONTH_COUNT)
        {
            return;
        }

        Simulation.I.StartSchedule(_selectedSchedules);
        ResetSchedule();
    }

    public void UpdateUI()
    {
        if (Simulation.I.IsGameEnded)
        {
            _monthText.text = $"게임 종료";
        }
        else
        {
            _monthText.text = $"{Simulation.I.GetMonth()}월";
        }
        _swimmerInfo.UpdateSlot();
        _calendarSlot.SetCalender(_selectedSchedules);
        _costPreviewSlot.SetCostPreview(_goldPreview, Simulation.I.Swimmer.GetStat(StatType.Stamina));
        SetScheduleButtons();
    }

    private void SetScheduleButtons()
    {
        var scheduleDatas = GameData.I.Schedule.SelectableDatas;
        for (int i = 0; i < scheduleDatas.Count; i++)
        {
            var data = scheduleDatas[i];
            _scheduleButtonEntries[i].SetButton(data, _goldPreview);
        }
    }

    private int GetTotalGoldCost()
    {
        int gold = 0;
        for (int i = 0; i < _currentScheduleIndex; i++)
        {
            if (_selectedSchedules[i] != ScheduleType.Match)
            {
                gold += GameData.I.Schedule.GetData(_selectedSchedules[i]).GoldCost * Constant.DAY_PER_WEEK_COUNT;
            }
        }
        return gold;
    }

    private void CheckFixedAndMoveIndex()
    {
        if (_currentScheduleIndex >= _selectedSchedules.Length)
        {
            return;
        }

        if (_selectedSchedules[_currentScheduleIndex] != ScheduleType.Invalid)
        {
            _currentScheduleIndex += 1;
        }
    }
}
