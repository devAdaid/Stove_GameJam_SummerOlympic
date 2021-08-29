using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStatData
{
    public readonly int Index;
    public readonly string Name;
    public readonly int Endurance;
    public readonly int Quickness;
    public readonly int Strength;
    public readonly int Flexibility;
    public readonly int [] TapSpeeds;
    public readonly int FlagType;
    public readonly int DiveStat;
    public AIStatData( int index, string name,
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
    List<AIStatData> datas = new List<AIStatData>();
    public AIStatDatas()
    {
        var parsedDatas = CSVParser.ReadFromFile(ResourcePath.AISTAT_DATA);
        int index = 0;
        foreach (var line in parsedDatas)
        {
            string name = (string)line["이름"];
            int endurance = (int)line["지구력"];
            int quickness = (int)line["순발력"];
            int strength = (int)line["근력"];
            int flexibility = (int)line["유연성"];
            int[] tapSpeed = new int[4];
            tapSpeed[0] = (int)line["스타트게임"];
            tapSpeed[1] = (int)line["25-50"];
            tapSpeed[2] = (int)line["50-75"];
            tapSpeed[3] = (int)line["75-100"];
            int flagType = 0;
            string country = (string)line["국가"];
            if (country == "호주")
                flagType = 1;
            else if (country == "일본")
                flagType = 2;
            else if (country == "네덜란드")
                flagType = 3;
            else if (country == "남아프리카")
                flagType = 4;
            int diveStat = (int)line["스타트게임"];
            AIStatData data = new AIStatData(index, name, endurance, quickness, strength, flexibility,tapSpeed, flagType, diveStat );
            datas.Add(data);
        }
    }
}
