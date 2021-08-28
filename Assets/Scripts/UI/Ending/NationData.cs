using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class NationData
{
    public string Name;
    public Sprite FlagSprite;

    public NationData(string name, Sprite flagSprite)
    {
        Name = name;
        FlagSprite = flagSprite;
    }
}

public class NationDatas : Singleton<NationDatas>
{
    public List<NationData> nationDatas = new List<NationData>();

    public NationDatas()
    {
        var parsedDatas = CSVParser.ReadFromFile(ResourcePath.NATION_DATA);
        foreach (var line in parsedDatas)
        {
            var nationName = (string)line["국가명"];
            var flagSpriteName = (string)line["리소스명"];
            var flagSprite = Resources.Load<Sprite>(Path.Combine(ResourcePath.FLAG_ICON_SPRITE, flagSpriteName));
            NationData data = new NationData(nationName, flagSprite);

            nationDatas.Add(data);
        }

    }
}
