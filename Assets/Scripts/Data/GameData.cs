using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameData : Singleton<GameData>
{
    public readonly MBTIDatas MBTI;

    public GameData()
    {
        MBTI = new MBTIDatas();
    }
}

public class MBTIDatas
{
    public List<MBTIData> Datas => _datas.Values.ToList();
    private readonly Dictionary<string, MBTIData> _datas = new Dictionary<string, MBTIData>();

    public MBTIDatas()
    {
        var parsedDatas = CSVParser.ReadFromFile(Path.MBTI_DATA);
        foreach (var line in parsedDatas)
        {
            var mbti = (string)line["MBTI"];
            var endurance = (int)line["심폐지구력"];
            var quickness = (int)line["순발력"];
            var strength = (int)line["근력"];
            var flexibility = (int)line["유연성"];
            var data = new MBTIData(mbti, endurance, quickness, strength, flexibility);
            _datas.Add(mbti, data);
        }
    }

    /// <summary>
    /// MBTI에 따른 기본 스탯을 받아옵니다.
    /// </summary>
    public Dictionary<StatType, int> GetDefaultStats(MBTI mbti)
    {
        var stats = new Dictionary<StatType, int>();
        stats.Add(StatType.Stamina, Constant.DEFAULT_STAMINA);

        var mbtiKey = mbti.ToString();
        if (_datas.TryGetValue(mbtiKey, out var data))
        {
            stats.Add(StatType.Endurance, data.Endurance);
            stats.Add(StatType.Quickness, data.Quickness);
            stats.Add(StatType.Strength, data.Strength);
            stats.Add(StatType.Flexibility, data.Flexibility);
        }

        Debug.LogWarning($"MBTI 스탯 데이터를 찾을 수 없었습니다: {mbtiKey}");
        return stats;
    }
}
