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

    public void SetButton(ScheduleData data)
    {
        _scheduleType = data.ScheduleType;
        _iconImage.sprite = data.IconSprite;
        _nameText.text = data.DisplayName;
        _goldCostText.text = $"-{data.GoldCost}G";
        _staminaCostText.text = $"-{data.StaminaCost}S";
    }

    public void SelectSchedule()
    {
        ScheduleSelectUI.I.SelectSchedule(_scheduleType);
    }
}
