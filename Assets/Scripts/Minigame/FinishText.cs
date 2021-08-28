using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishText : MonoBehaviour
{
    Text text;
    private void Awake()
    {
        text = GetComponent<Text>();
    }
    public void Start()
    {
        StartCoroutine(FinishCount());
    }
    IEnumerator FinishCount()
    {
        float duration = 0.6f;
        float eTime = 0f;
        while (eTime < duration)
        {
            text.rectTransform.localScale = new Vector3(1, 1, 1) * TimeCurves.ExponentialMirrored(eTime / duration);
            text.color = new Color(text.color.r, text.color.g, text.color.b, 1 - TimeCurves.Exponential(eTime / duration));
            yield return null;
            eTime += Time.deltaTime;
        }
        Destroy(gameObject);
    }
}
