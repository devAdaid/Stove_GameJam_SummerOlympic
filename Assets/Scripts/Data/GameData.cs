using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameData : Singleton<GameData>
{
    public readonly MBTIDatas MBTI;
    public readonly ScheduleDatas Schedule;
    public readonly FixedScheduleDatas FixedSchedule;
    public readonly AIStatDatas AIStat;
    public readonly StoreItemDatas StoreItem;

    public GameData()
    {
        MBTI = new MBTIDatas();
        Schedule = new ScheduleDatas();
        FixedSchedule = new FixedScheduleDatas();
        AIStat = new AIStatDatas();
        StoreItem = new StoreItemDatas();
    }
}