using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FixedScheduleData
{
    public readonly int Month;
    public readonly int Week;
    public readonly string DisplayName;
    public readonly Sprite IconSprite;
    public readonly Dictionary<int, int> RewardGoldByRank;

    public FixedScheduleData(int month, int week, string displayName, Sprite iconSprite, Dictionary<int, int> rewardGoldByRank)
    {
        Month = month;
        Week = week;
        DisplayName = displayName;
        IconSprite = iconSprite;
        RewardGoldByRank = rewardGoldByRank;
    }

    public int GetGoldRewardByRank(int rank)
    {
        if (RewardGoldByRank.TryGetValue(rank, out var reward))
        {
            return reward;
        }

        return 0;
    }
}

public class FixedScheduleDatas
{
    private readonly Dictionary<int, FixedScheduleData> _datas = new Dictionary<int, FixedScheduleData>();

    public FixedScheduleDatas()
    {
        var parsedDatas = CSVParser.ReadFromFile(ResourcePath.FIXED_SCHEDULE_DATA);
        foreach (var line in parsedDatas)
        {
            var month = (int)line["Month"];
            var week = (int)line["Week"];

            var displayName = (string)line["DisplayName"];

            var iconSpriteName = (string)line["IconSpriteName"];
            var iconSprite = Resources.Load<Sprite>(Path.Combine(ResourcePath.SCHEDULE_ICON_SPRITE, iconSpriteName));

            var rewardGold_1 = (int)line["RewardGold_1st"];
            var rewardGold_2 = (int)line["RewardGold_2nd"];
            var rewardGold_3 = (int)line["RewardGold_3rd"];

            var rewardGoldByRank = new Dictionary<int, int>();
            rewardGoldByRank.Add(1, rewardGold_1);
            rewardGoldByRank.Add(2, rewardGold_2);
            rewardGoldByRank.Add(3, rewardGold_3);

            var data = new FixedScheduleData(month, week, displayName, iconSprite, rewardGoldByRank);
            _datas.Add(month, data);
        }
    }

    public bool TryGetData(int month, out FixedScheduleData fixedScheduleData)
    {
        if (_datas.TryGetValue(month, out var data))
        {
            fixedScheduleData = data;
            return true;
        }

        fixedScheduleData = null;
        return false;
    }
}