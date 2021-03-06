using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwimStatEntry : MonoBehaviour
{
    [SerializeField]
    private StatType _statType;
    [SerializeField]
    private Text _statNameText;
    [SerializeField]
    private Image _statGaugeFillImage;
    [SerializeField]
    private Text _statValueText;

    public void UpdateEntry()
    {
        _statNameText.text = _statType.GetString();

        var currentStat = Simulation.I.Swimmer.GetStat(_statType);
        var maxStat = Constant.SWIMSTAT_MAX;
        _statGaugeFillImage.fillAmount = (float)currentStat / maxStat;

        _statValueText.text = currentStat.ToString();
    }
}
