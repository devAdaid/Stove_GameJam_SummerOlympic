using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreItemData
{
    public readonly string Item;
    public readonly int Endurance;
    public readonly int Quickness;
    public readonly int Strength;
    public readonly int Flexibility;
    public readonly int Stamina;
    public readonly int GoldCost;

    public StoreItemData(
        string item,
        int endurance, int quickness,
        int strength, int flexibility,
        int stamina, int goldCost)
    {
        Item = item;
        Endurance = endurance;
        Quickness = quickness;
        Strength = strength;
        Flexibility = flexibility;
        Stamina = stamina;
        GoldCost = goldCost;
    }
}


public class StoreItemDatas
{
    string filePath;
    public List<StoreItemData> Datas { get { return datas; } }
    List<StoreItemData> datas = new List<StoreItemData>();

    public StoreItemDatas()
    {
        filePath = Application.dataPath + "/" + "StoreItem.csv";
        //var parsedDatas = CSVParser.ReadFromFile(ResourcePath.StoreItem_Data);
        var parsedDatas = CSVParser.Read(filePath);
        foreach (var line in parsedDatas)
        {
            var item = (string)line["아이템 이름"];
            var endurance = (int)line["심폐지구력"];
            var quickness = (int)line["순발력"];
            var strength = (int)line["근력"];
            var flexibility = (int)line["유연성"];
            var stamina = (int)line["체력"];
            var goldCost = (int)line["가격"];
            StoreItemData data = new StoreItemData(item, endurance, quickness, strength, flexibility, stamina, goldCost);
            datas.Add(data);
        }
    }
}

