using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SwimmerInfoSlot : MonoBehaviour
{
    [SerializeField]
    private Text _nameText;
    [SerializeField]
    private List<SwimStatEntry> _swimStatEntries;

    public void UpdateSlot()
    {
        _nameText.text = Simulation.I.Swimmer.Name;
        foreach (var entry in _swimStatEntries)
        {
            entry.UpdateEntry();
        }
    }
}
