using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Simulation : Singleton<Simulation>
{
    public int PassedMonth { get; private set; }
    public bool IsGameEnded => PassedMonth > Constant.MONTH_COUNT;
    public SwimmerCharacter Swimmer { get; private set; }
    public int Gold { get; private set; }
    public int Day = 0;
    private ScheduleType[] _selectedSchedules;
    public static bool IsMiniGameEnded = false;
    private ProcessScheduleRequestData _requestData = null;
    private string _scheduleName = string.Empty;
    public Simulation()
    {
        Swimmer = new SwimmerCharacter();
        PassedMonth = 0;
        Day = 0;
        _selectedSchedules = null;
    }

    public void Reset()
    {
        Swimmer = new SwimmerCharacter();
        Gold = 0;
        PassedMonth = 0;
        Day = 0;
        _selectedSchedules = null;
    }

    public int GetMonth()
    {
        var month = PassedMonth + Constant.INITIAL_MONTH;
        if (month > 12)
        {
            return month - 12;
        }
        return month;
    }

    public void SetBaseStat(Dictionary<StatType, int> stats)
    {
        Swimmer.SetBaseStat(stats);
    }

    public void SetSwimmerName(string name)
    {
        Swimmer.SetName(name);
    }

    public void IncreaseSwimmerStat(StatType statType, int value)
    {
        Swimmer.IncreaseStat(statType, value);
    }

    public void AddGold(int value)
    {
        Gold += value;
    }

    public bool CanPayGold(int value)
    {
        return Gold >= value;
    }

    public bool DecreaseGold(int value)
    {
        if (!CanPayGold(value))
        {
            return false;
        }

        Gold -= value;
        return true;
    }

    public void DecreaseStamina(int value)
    {
        Swimmer.DecreaseStat(StatType.Stamina, value);
    }

    public void OnScheduleScene()
    {
        if (IsMiniGameEnded)
        {
            IsMiniGameEnded = false;
            OnMiniGameEnd();
        }
    }

    public void StartSchedule(ScheduleType[] selectedSchedules)
    {
        Day = 1;
        _selectedSchedules = new ScheduleType[Constant.WEEK_PER_MONTH_COUNT];
        for (int i = 0; i < Constant.WEEK_PER_MONTH_COUNT; i++)
        {
            _selectedSchedules[i] = selectedSchedules[i];
        }

        CheckAndProcessSchedule();
    }

    public void OnProcessScheduleEnd()
    {
        Day += 1;
        CheckAndProcessSchedule();
    }

    public void OnMiniGameEnd()
    {
        Day += Constant.DAY_PER_WEEK_COUNT;
        CheckAndProcessSchedule();
    }

    private void CheckAndProcessSchedule()
    {
        if (Day > Constant.DAY_PER_MONTH_COUNT)
        {
            // 다음 달로 진행한다.
            PassedMonth += 1;
            Day = 0;
            SchedueProgressRoot.I.SetActive(false);
            ScheduleSelectUI.I.ResetSchedule();
        }
        else
        {
            RequestProcessSchedule();
        }
    }

    private void RequestProcessSchedule()
    {
        // 오늘 어떤 일정인지 확인한다.
        var weekIndex = (Day - 1) / Constant.DAY_PER_WEEK_COUNT;
        var scheduleType = _selectedSchedules[weekIndex];

        // 경기일 경우 미니게임 플레이, 플레이 이후 7일 건너뛸것
        if (scheduleType == ScheduleType.Match)
        {
            var month = GetMonth();
            SwimGameManager.SwimmerIndicies = GameData.I.AIStat.GetSwimmerIndices(month);

            if (month == 7)
            {
                SwimGameManager.isLastGame = true;
            }

            SceneManager.LoadScene("3_Minigame");
        }
        // 아닐 경우 일반 일정을 진행.
        else
        {
            ScheduleData scheduleData = null;
            if (Swimmer.GetStat(StatType.Stamina) > 0
                || scheduleType == ScheduleType.Rest)
            {
                scheduleData = GameData.I.Schedule.GetData(scheduleType);
            }
            else
            {
                scheduleData = GameData.I.Schedule.GetData(ScheduleType.Rest_Forced);
            }
            _requestData = MakeProcessRequestData(scheduleData);
            _scheduleName = scheduleData.DisplayName;

            SchedueProgressRoot.I.SetActive(true);
            SchedueProgressRoot.I.Progress.ProcessSchedule(_requestData, _scheduleName, Day);
        }
    }

    private ProcessScheduleRequestData MakeProcessRequestData(ScheduleData scheduleData)
    {
        Dictionary<StatType, int> statChanges = new Dictionary<StatType, int>();

        int goldDiff = 0;
        if (scheduleData.GoldCost > 0)
        {
            goldDiff -= scheduleData.GoldCost;
        }

        if (scheduleData.StaminaCost > 0)
        {
            statChanges.Add(StatType.Stamina, -scheduleData.StaminaCost);
        }

        foreach (var reward in scheduleData.RewardTypes)
        {
            AddRequestdChange(statChanges, reward, scheduleData.MinRewardValue, scheduleData.MaxRewardValue, scheduleData.RewardValueStep, out var rewardGoldDiff);
            if (rewardGoldDiff > 0)
            {
                goldDiff = rewardGoldDiff;
            }
        }

        var statChangedInfo = statChanges.Select(c => new StatChangedInfo(c.Key, c.Value)).ToList();
        return new ProcessScheduleRequestData(statChangedInfo, goldDiff);
    }

    private void AddRequestdChange(Dictionary<StatType, int> statChanges, ScheduleRewardType rewardType, int minValue, int maxValue, int stepValue, out int goldDiff)
    {
        goldDiff = 0;
        switch (rewardType)
        {
            case ScheduleRewardType.Stat_Stamina:
                AddStatChange(statChanges, StatType.Stamina, minValue, maxValue, stepValue);
                break;
            case ScheduleRewardType.SwimStat_Endurance:
                AddStatChange(statChanges, StatType.Endurance, minValue, maxValue, stepValue);
                break;
            case ScheduleRewardType.SwimStat_Quickness:
                AddStatChange(statChanges, StatType.Quickness, minValue, maxValue, stepValue);
                break;
            case ScheduleRewardType.SwimStat_Strength:
                AddStatChange(statChanges, StatType.Strength, minValue, maxValue, stepValue);
                break;
            case ScheduleRewardType.SwimStat_Flexibility:
                AddStatChange(statChanges, StatType.Flexibility, minValue, maxValue, stepValue);
                break;
            case ScheduleRewardType.SwimStat_All:
                AddStatChange(statChanges, StatType.Endurance, minValue, maxValue, stepValue);
                AddStatChange(statChanges, StatType.Quickness, minValue, maxValue, stepValue);
                AddStatChange(statChanges, StatType.Strength, minValue, maxValue, stepValue);
                AddStatChange(statChanges, StatType.Flexibility, minValue, maxValue, stepValue);
                break;
            case ScheduleRewardType.Gold:
                var randomValue = GetRandomValue(minValue, maxValue, stepValue);
                goldDiff = randomValue;
                break;
            default:
                Debug.LogError($"보상을 전달할 수 없었습니다.");
                break;
        }
    }

    private void AddStatChange(Dictionary<StatType, int> statChanges, StatType statType, int minValue, int maxValue, int stepValue)
    {
        var randomValue = GetRandomValue(minValue, maxValue, stepValue);
        statChanges.Add(statType, randomValue);
    }

    private int GetRandomValue(int minValue, int maxValue, int stepValue)
    {
        var stepCount = ((maxValue - minValue) / stepValue) + 1;
        var randomStep = Random.Range(0, stepCount);
        return minValue + randomStep * stepValue;
    }

    private FixedScheduleData GetMatchData()
    {
        var month = GetMonth();
        if (GameData.I.FixedSchedule.TryGetData(month, out var data))
        {
            return data;
        }
        return null;
    }

    public void TempDoSchedule(ScheduleType[] schedules)
    {
        if (IsGameEnded)
        {
            return;
        }

        if (schedules.Count(s => s == ScheduleType.Invalid) > 0)
        {
            Debug.LogError($"스케쥴이 4개 미만으로 선택되었습니다.");
            return;
        }

        foreach (var schedule in schedules)
        {
            if (schedule == ScheduleType.Match)
            {
                continue;
            }

            var scheduleData = GameData.I.Schedule.GetData(schedule);
            foreach (var reward in scheduleData.RewardTypes)
            {
                for (int i = 0; i < Constant.DAY_PER_WEEK_COUNT; i++)
                {
                    DeliverReward(reward, scheduleData.MinRewardValue, scheduleData.MaxRewardValue, scheduleData.RewardValueStep);
                }
            }
            DecreaseGold(scheduleData.GoldCost * Constant.DAY_PER_WEEK_COUNT);
            DecreaseStamina(scheduleData.StaminaCost * Constant.DAY_PER_WEEK_COUNT);
        }

        AddGold(Constant.GOLD_DELIVERED_AT_MONTH);
        PassedMonth += 1;
    }

    private void DeliverReward(ScheduleRewardType rewardType, int minValue, int maxValue, int stepValue)
    {
        switch (rewardType)
        {
            case ScheduleRewardType.Stat_Stamina:
                IncreaseSwimmerStat(StatType.Stamina, GetRandomValue(minValue, maxValue, stepValue));
                break;
            case ScheduleRewardType.SwimStat_Endurance:
                IncreaseSwimmerStat(StatType.Endurance, GetRandomValue(minValue, maxValue, stepValue));
                break;
            case ScheduleRewardType.SwimStat_Quickness:
                IncreaseSwimmerStat(StatType.Quickness, GetRandomValue(minValue, maxValue, stepValue));
                break;
            case ScheduleRewardType.SwimStat_Strength:
                IncreaseSwimmerStat(StatType.Strength, GetRandomValue(minValue, maxValue, stepValue));
                break;
            case ScheduleRewardType.SwimStat_Flexibility:
                IncreaseSwimmerStat(StatType.Flexibility, GetRandomValue(minValue, maxValue, stepValue));
                break;
            case ScheduleRewardType.SwimStat_All:
                IncreaseSwimmerStat(StatType.Endurance, GetRandomValue(minValue, maxValue, stepValue));
                IncreaseSwimmerStat(StatType.Quickness, GetRandomValue(minValue, maxValue, stepValue));
                IncreaseSwimmerStat(StatType.Strength, GetRandomValue(minValue, maxValue, stepValue));
                IncreaseSwimmerStat(StatType.Flexibility, GetRandomValue(minValue, maxValue, stepValue));
                break;
            case ScheduleRewardType.Gold:
                AddGold(GetRandomValue(minValue, maxValue, stepValue));
                break;
            default:
                Debug.LogError($"보상을 전달할 수 없었습니다.");
                break;
        }
    }
}