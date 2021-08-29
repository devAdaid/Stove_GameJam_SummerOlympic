using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerRecord
{
    public string playerName;
    public string nationName;
    public float record;
}

public class OlympicRecordData : Singleton<OlympicRecordData>
{
    public List<PlayerRecord> playerRecords;

    public void SetPlayerRecord(List<PlayerRecord> records)
    {
        playerRecords = records;
        playerRecords.Sort((recordA, recordB) => recordA.record.CompareTo(recordB.record));
    }
}
