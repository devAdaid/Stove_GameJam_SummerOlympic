using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressStat : MonoBehaviour
{
    [SerializeField]
    private List<SwimStatEntry> _swimStatEntries;
    [SerializeField]
    private Text _goldText;

    public void UpdateUI()
    {
        foreach (var entry in _swimStatEntries)
        {
            entry.UpdateEntry();
        }
        _goldText.text = Simulation.I.Gold.ToString();
    }
}
