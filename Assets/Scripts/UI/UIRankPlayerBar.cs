using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRankPlayerBar : MonoBehaviour
{
    public Image medalImage;
    public Image nationFlagImage;

    public Text nationKoreanNameText;
    public Text playerNameText;

    public void UpdateUI(string playterName, string nationName)
    {
        playerNameText.text = playterName;

        int index = NationDatas.I.nationDatas.FindIndex(x => x.Name == nationName);

        nationFlagImage.sprite = NationDatas.I.nationDatas[index].FlagSprite;
        nationKoreanNameText.text = NationDatas.I.nationDatas[index].Name;
    }
}
