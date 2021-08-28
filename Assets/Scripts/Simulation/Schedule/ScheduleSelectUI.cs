using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScheduleSelectUI : MonoSingleton<ScheduleSelectUI>
{
    [SerializeField]
    private CalendarSlot _calendarSlot;
    [SerializeField]
    private CostPreviewSlot _costPreviewSlot;
    [SerializeField]
    private Transform _scheduleButtonRoot;
    [SerializeField]
    private ScheduleButtonEntry _scheduleButtonPrefab;
    private ScheduleType[] _selectedSchedules = new ScheduleType[Constant.WEEK_PER_MONTH_COUNT];
    private int _selectedScheduleCount = 0;
    private List<ScheduleButtonEntry> _scheduleButtonEntries = new List<ScheduleButtonEntry>();
    private int _goldPreview => Simulation.I.Gold - GetTotalGoldCost();
    private int _staminaPreview => Simulation.I.Swimmer.GetStat(StatType.Stamina) - GetTotalStamina();

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
        var scheduleData = GameData.I.Schedule.GetData(schedule);
        if (_goldPreview < scheduleData.GoldCost)
        {
            return;
        }

        if (_selectedScheduleCount < _selectedSchedules.Length)
        {
            _selectedSchedules[_selectedScheduleCount] = schedule;
            _selectedScheduleCount += 1;
        }

        UpdateUI();
    }

    public void ResetSchedule()
    {
        for (int i = 0; i < _selectedSchedules.Length; i++)
        {
            _selectedSchedules[i] = ScheduleType.Invalid;
        }

        _selectedScheduleCount = 0;

        UpdateUI();
    }

    private void UpdateUI()
    {
        _calendarSlot.SetCalender(_selectedSchedules);
        _costPreviewSlot.SetCostPreview(_goldPreview, _staminaPreview);
        SetScheduleButtons();
    }

    private void SetScheduleButtons()
    {
        var scheduleDatas = GameData.I.Schedule.SelectableDatas;
        bool isSteminaZero = _staminaPreview == 0;
        for (int i = 0; i < scheduleDatas.Count; i++)
        {
            var data = scheduleDatas[i];
            if (isSteminaZero)
            {
                data = GameData.I.Schedule.GetData(ScheduleType.Rest_Forced);
            }
            _scheduleButtonEntries[i].SetButton(data, _goldPreview);
        }
    }

    private int GetTotalGoldCost()
    {
        int gold = 0;
        for (int i = 0; i < _selectedScheduleCount; i++)
        {
            gold += GameData.I.Schedule.GetData(_selectedSchedules[i]).GoldCost;
        }
        return gold;
    }

    private int GetTotalStamina()
    {
        int stamina = 0;
        for (int i = 0; i < _selectedScheduleCount; i++)
        {
            stamina += GameData.I.Schedule.GetData(_selectedSchedules[i]).StaminaCost;
        }
        return stamina;
    }
}
