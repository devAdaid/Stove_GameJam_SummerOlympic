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
            var scheduleData = GameData.I.Schedule.GetData(schedule);
            foreach (var reward in scheduleData.RewardTypes)
            {
                DeliverReward(reward, scheduleData.MaxRewardValue * 7);
            }
            DecreaseGold(scheduleData.GoldCost);
            DecreaseStamina(scheduleData.StaminaCost);
        }

        PassedMonth += 1;
    }

    private void DeliverReward(ScheduleRewardType rewardType, int value)
    {
        switch (rewardType)
        {
            case ScheduleRewardType.Stat_Stamina:
                IncreaseSwimmerStat(StatType.Stamina, value);
                break;
            case ScheduleRewardType.SwimStat_Endurance:
                IncreaseSwimmerStat(StatType.Endurance, value);
                break;
            case ScheduleRewardType.SwimStat_Quickness:
                IncreaseSwimmerStat(StatType.Quickness, value);
                break;
            case ScheduleRewardType.SwimStat_Strength:
                IncreaseSwimmerStat(StatType.Strength, value);
                break;
            case ScheduleRewardType.SwimStat_Flexibility:
                IncreaseSwimmerStat(StatType.Flexibility, value);
                break;
            case ScheduleRewardType.SwimStat_All:
                IncreaseSwimmerStat(StatType.Endurance, value);
                IncreaseSwimmerStat(StatType.Quickness, value);
                IncreaseSwimmerStat(StatType.Strength, value);
                IncreaseSwimmerStat(StatType.Flexibility, value);
                break;
            case ScheduleRewardType.Gold:
                AddGold(value);
                break;
            default:
                Debug.LogError($"보상을 전달할 수 없었습니다.");
                break;
        }
    }
}