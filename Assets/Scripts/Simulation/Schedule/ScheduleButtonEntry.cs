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

        var totalStaminaCost = data.StaminaCost * Constant.DAY_PER_WEEK_COUNT;
        _staminaCostText.text = $"-{totalStaminaCost}S";

        var totalGoldCost = data.GoldCost * Constant.DAY_PER_WEEK_COUNT;
        if (currentGoldPreview < totalGoldCost)
        {
            _goldCostText.text = $"<color=red>-{totalGoldCost}G</color>";
        }
        else
        {
            _goldCostText.text = $"-{totalGoldCost}G";
        }
    }

    public void SelectSchedule()
    {
        ScheduleSelectUI.I.SelectSchedule(_scheduleType);
    }
}
