using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Simulation : Singleton<Simulation>
{
    public int PassedMonth { get; private set; }
    public bool IsGameEnded => PassedMonth > Constant.MONTH_COUNT;
    public SwimmerCharacter Swimmer { get; private set; }
    public int Gold { get; private set; }

    public Simulation()
    {
        Swimmer = new SwimmerCharacter();
        PassedMonth = 0;
    }

    public void Reset()
    {
        Swimmer = new SwimmerCharacter();
        Gold = 0;
        PassedMonth = 0;
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

    private int GetRandomValue(int minValue, int maxValue, int stepValue)
    {
        var stepCount = ((maxValue - minValue) / stepValue) + 1;
        var randomStep = Random.Range(0, stepCount);
        return minValue + randomStep * stepValue;
    }
}