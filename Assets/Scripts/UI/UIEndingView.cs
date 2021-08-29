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

        for(int i = 0; i < rankPlayerBars.Length; i++)
        {
            var playerName = OlympicRecordData.I.playerRecords[i].playerName;
            var nationName = OlympicRecordData.I.playerRecords[i].nationName;
            rankPlayerBars[i].UpdateUI(playerName, nationName);
        }

        bool isPlayerNoAward = true;

        if (isPlayerNoAward)
        {
            rankPlayerBars[3].gameObject.SetActive(true);
            rankPlayerBars[3].UpdateUI("Test", "´ëÇÑ¹Î±¹");
        }
    }

}
