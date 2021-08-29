using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEndingView : UIView
{
    public UIRankPlayerBar[] rankPlayerBars;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        UpdateUI();
    }


    private void UpdateUI()
    {
#if UNITY_EDITOR
        if (OlympicRecordData.I.playerRecords == null)
        {
            Debug.LogError("playerRecords does not exists");
            return;
        }
#endif

        for (int i = 0; i < rankPlayerBars.Length; i++)
        {
            var playerName = OlympicRecordData.I.playerRecords[i].playerName;
            var nationName = OlympicRecordData.I.playerRecords[i].nationName;
            rankPlayerBars[i].UpdateUI(playerName, nationName);
        }

        bool isPlayerNoAward = true;

        int playerRank = OlympicRecordData.I.playerRecords.FindIndex(x => x.playerName == Simulation.I.Swimmer.Name);

        if (playerRank >= 3)
        {
            rankPlayerBars[3].gameObject.SetActive(true);
            rankPlayerBars[3].UpdateUI(Simulation.I.Swimmer.Name, "대한민국");
            AudioManager.I.PlaySfx(SfxType.Success);
        }
    }

}
