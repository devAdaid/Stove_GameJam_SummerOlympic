using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    [SerializeField] Text text;
    public IEnumerator StartCountDown(float speed)
    {
        gameObject.SetActive(true);
        yield return StartCoroutine(ScaleCoroutine(speed));
        text.text = "2";
        yield return StartCoroutine(ScaleCoroutine(speed));
        text.text = "1";
        yield return StartCoroutine(ScaleCoroutine(speed));

        StartCoroutine(FinishCount());
    }

    IEnumerator FinishCount()
    {
        float duration = 0.6f;
        float eTime = 0f;
        text.text = "Go!";
        while(eTime < duration)
        {
            text.rectTransform.localScale = new Vector3(1, 1, 1) * TimeCurves.ExponentialMirrored(eTime / duration);
            text.color = new Color(text.color.r, text.color.g, text.color.b, 1 - TimeCurves.Exponential(eTime / duration));
            yield return null;
            eTime += Time.deltaTime;
        }
        Destroy(gameObject);
    }
    IEnumerator ScaleCoroutine(float speed)
    {
        float eTime = 0f;
        while (eTime < 0.3f)
        {
            text.rectTransform.localScale = new Vector3(1, 1, 1) * TimeCurves.ExponentialMirrored(eTime / 0.3f);
            yield return null;
            eTime += Time.deltaTime * speed;
        }
        text.rectTransform.localScale = new Vector3(1, 1, 1);
        while (eTime < 1f)
        {
            yield return null;
            eTime += Time.deltaTime * speed;
        }
    }
}
