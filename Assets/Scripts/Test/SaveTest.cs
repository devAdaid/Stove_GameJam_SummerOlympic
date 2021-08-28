using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveTest : MonoBehaviour
{
    public SaveManager saveManager;
    public SaveData saveData;

    public void Save()
    {
        saveData.statTable[StatType.Endurance] = saveData.playerGold;

        SaveManager.Save(saveData);
    }
    public void Load()
    {
        saveData = (SaveData)SaveManager.Load();

        Debug.Log(saveData.statTable[StatType.Endurance].ToString());
    }
}
