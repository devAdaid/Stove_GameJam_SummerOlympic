using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

public class ScheduleData
{
    public readonly ScheduleType ScheduleType;
    public readonly string DisplayName;
    public readonly Sprite IconSprite;
    public readonly int GoldCost;
    public readonly int StaminaCost;
    public readonly List<ScheduleRewardType> RewardTypes;
    public readonly int MinRewardValue;
    public readonly int MaxRewardValue;
    public readonly int RewardValueStep;

    public ScheduleData(ScheduleType scheduleType, string displayName, Sprite iconSprite,
        int goldCost, int staminaCost,
         List<ScheduleRewardType> rewardTypes, int minRewardValue, int maxRewardValue, int rewardValueStep)
    {
        ScheduleType = scheduleType;
        DisplayName = displayName;
        IconSprite = iconSprite;
        GoldCost = goldCost;
        StaminaCost = staminaCost;
        RewardTypes = rewardTypes;
        MinRewardValue = minRewardValue;
        MaxRewardValue = maxRewardValue;
        RewardValueStep = rewardValueStep;

        Assert.IsTrue((MaxRewardValue - MinRewardValue) % RewardValueStep == 0, "RewardValue값이 이상한 것 같습니다.");
    }
}

public class ScheduleDatas
{
    private readonly Dictionary<ScheduleType, ScheduleData> _datas = new Dictionary<ScheduleType, ScheduleData>();
    public readonly List<ScheduleData> SelectableDatas = new List<ScheduleData>();

    public ScheduleDatas()
    {
        var parsedDatas = CSVParser.ReadFromFile(ResourcePath.SCHEDULE_DATA);
        foreach (var line in parsedDatas)
        {
            var scheduleType = (ScheduleType)Enum.Parse(typeof(ScheduleType), (string)line["ScheduleType"]);
            var displayName = (string)line["DisplayName"];

            var iconSpriteName = (string)line["IconSpriteName"];
            var iconSprite = Resources.Load<Sprite>(Path.Combine(ResourcePath.SCHEDULE_ICON_SPRITE, iconSpriteName));

            var goldCost = (int)line["GoldCost"];
            var staminaCost = (int)line["StaminaCost"];

            var rewardType0 = ParseRewardType((string)line["RewardType0"]);
            var rewardType1 = ParseRewardType((string)line["RewardType1"]);
            var rewardTypes = new List<ScheduleRewardType>();
            if (rewardType0 != ScheduleRewardType.Invalid) rewardTypes.Add(rewardType0);
            if (rewardType1 != ScheduleRewardType.Invalid) rewardTypes.Add(rewardType1);

            var minRewardValue = (int)line["MinRewardValue"];
            var maxRewardValue = (int)line["MaxRewardValue"];
            var rewardStep = (int)line["RewardStep"];

            var data = new ScheduleData(scheduleType, displayName, iconSprite, goldCost, staminaCost, rewardTypes, minRewardValue, maxRewardValue, rewardStep);
            _datas.Add(scheduleType, data);

            if (scheduleType != ScheduleType.Rest_Forced)
            {
                SelectableDatas.Add(data);
            }
        }
    }

    public ScheduleData GetData(ScheduleType scheduleType)
    {
        if (_datas.TryGetValue(scheduleType, out var data))
        {
            return data;
        }

        Debug.LogError($"{scheduleType}의 데이터를 찾을 수 없습니다.");
        return null;
    }

    private ScheduleRewardType ParseRewardType(string rewardText) =>
    rewardText switch
    {
        "체력" => ScheduleRewardType.Stat_Stamina,
        "근력" => ScheduleRewardType.SwimStat_Strength,
        "유연성" => ScheduleRewardType.SwimStat_Flexibility,
        "지구력" => ScheduleRewardType.SwimStat_Endurance,
        "순발력" => ScheduleRewardType.SwimStat_Quickness,
        "전체 수영스탯" => ScheduleRewardType.SwimStat_All,
        "골드" => ScheduleRewardType.Gold,
        _ => ScheduleRewardType.Invalid,
    };
}