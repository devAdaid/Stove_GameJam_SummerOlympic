using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIStatData
{
    public readonly int Index;
    public readonly string Name;
    public readonly int Endurance;
    public readonly int Quickness;
    public readonly int Strength;
    public readonly int Flexibility;
    public readonly int[] TapSpeeds;
    public readonly int FlagType;
    public readonly int DiveStat;
    public AIStatData(int index, string name,
        int endurance, int quickness,
        int strength, int flexibility, int[] tapSpeeds, int flagType, int diveStat)
    {
        Index = index;
        Name = name;
        Endurance = endurance;
        Quickness = quickness;
        Strength = strength;
        Flexibility = flexibility;
        TapSpeeds = tapSpeeds;
        FlagType = flagType;
        DiveStat = diveStat;
    }
}

public class AIStatDatas
{
    public List<AIStatData> Datas { get { return datas; } }
    private Dictionary<int, List<AIStatData>> _pairs = new Dictionary<int, List<AIStatData>>();
    List<AIStatData> datas = new List<AIStatData>();
    public AIStatDatas()
    {
        var parsedDatas = CSVParser.ReadFromFile(ResourcePath.AISTAT_DATA);
        int index = 0;
        foreach (var line in parsedDatas)
        {
            string name = (string)line["Name"];
            int endurance = (int)line["Endurance"];
            int quickness = (int)line["Quickness"];
            int strength = (int)line["Strength"];
            int flexibility = (int)line["Flexibility"];
            int[] tapSpeed = new int[4];
            tapSpeed[0] = (int)line["StartGame"];
            tapSpeed[1] = (int)line["25-50"];
            tapSpeed[2] = (int)line["50-75"];
            tapSpeed[3] = (int)line["75-100"];
            int flagType = 0;
            string country = (string)line["Country"];
            if (country == "Australia")
                flagType = 1;
            else if (country == "Japan")
                flagType = 2;
            else if (country == "Netherlands")
                flagType = 3;
            else if (country == "NorthAf")
                flagType = 4;
            int diveStat = (int)line["StartGame"];
            AIStatData data = new AIStatData(index, name, endurance, quickness, strength, flexibility, tapSpeed, flagType, diveStat);
            datas.Add(data);

            var month = (int)line["Month"];
            if (!_pairs.TryGetValue(month, out var list))
            {
                list = new List<AIStatData>();
                _pairs.Add(month, list);
            }
            list.Add(data);
        }
    }

    public int[] GetSwimmerIndices(int month)
    {
        return _pairs[month].Select(data => data.Index).ToArray();
    }
}
