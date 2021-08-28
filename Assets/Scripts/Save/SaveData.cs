using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public string playerName;

    public int passedMonth;

    public int playerGold;

    public Dictionary<StatType, int> statTable = new Dictionary<StatType, int>();

}
