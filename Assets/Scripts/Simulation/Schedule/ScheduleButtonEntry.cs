using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScheduleButtonEntry : MonoBehaviour
{
    [SerializeField]
    private Image _iconImage;
    [SerializeField]
    private Text _nameText;
    [SerializeField]
    private Text _goldCostText;
    [SerializeField]
    private Text _staminaCostText;

    private ScheduleType _scheduleType;

    public void SetButton(ScheduleData data, int currentGoldPreview)
    {
        _scheduleType = data.ScheduleType;
        _iconImage.sprite = data.IconSprite;
        _nameText.text = data.DisplayName;
        _staminaCostText.text = $"-{data.StaminaCost}S";

        if (currentGoldPreview < data.GoldCost)
        {
            _goldCostText.text = $"<color=red>-{data.GoldCost}G</color>";
        }
        else
        {
            _goldCostText.text = $"-{data.GoldCost}G";
        }
    }

    public void SelectSchedule()
    {
        ScheduleSelectUI.I.SelectSchedule(_scheduleType);
    }
}
