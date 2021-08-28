using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CostPreviewSlot : MonoBehaviour
{
    [SerializeField]
    private Text _goldCostText;
    [SerializeField]
    private Text _staminaCostText;

    public void SetCostPreview(int goldPreview, int staminaPreview)
    {
        if (staminaPreview < 0) staminaPreview = 0;

        _goldCostText.text = goldPreview.ToString();
        _staminaCostText.text = staminaPreview.ToString();
    }
}
