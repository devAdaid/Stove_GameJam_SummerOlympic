using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameData : Singleton<GameData>
{
    public readonly MBTIDatas MBTI;
    public readonly ScheduleDatas Schedule;

    public GameData()
    {
        MBTI = new MBTIDatas();
        Schedule = new ScheduleDatas();
    }
}