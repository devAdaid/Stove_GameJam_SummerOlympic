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
        _goldCostText.text = goldPreview.ToString();
        _staminaCostText.text = staminaPreview.ToString();
    }
}
