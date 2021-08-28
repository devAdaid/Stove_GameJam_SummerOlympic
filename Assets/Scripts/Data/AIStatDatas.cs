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
    public AIStatData( int index, string name,
        int endurance, int quickness,
        int strength, int flexibility, int[] tapSpeeds)
    {
        Index = index;
        Name = name;
        Endurance = endurance;
        Quickness = quickness;
        Strength = strength;
        Flexibility = flexibility;
        TapSpeeds = tapSpeeds;
    }
}

public class AIStatDatas : MonoBehaviour
{
    public List<AIStatData> Datas { get { return datas; } }
    List<AIStatData> datas = new List<AIStatData>();
    public AIStatDatas()
    {
        var parsedDatas = CSVParser.ReadFromFile(ResourcePath.AISTAT_DATA);
        int index = 0;
        foreach (var line in parsedDatas)
        {
            string name = (string)line["�̸�"];
            int endurance = (int)line["����������"];
            int quickness = (int)line["���߷�"];
            int strength = (int)line["�ٷ�"];
            int flexibility = (int)line["������"];
            int[] tapSpeed = new int[4];
            tapSpeed[0] = (int)line["��ŸƮ����"];
            tapSpeed[1] = (int)line["25-50"];
            tapSpeed[2] = (int)line["50-75"];
            tapSpeed[3] = (int)line["75-100"];
            AIStatData data = new AIStatData(index, name, endurance, quickness, strength, flexibility,tapSpeed );
            datas.Add(data);
        }
    }
}
