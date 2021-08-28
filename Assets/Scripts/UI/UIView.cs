using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIView : MonoBehaviour
{
    public bool isToggle;
    protected RectTransform rootRect;
    private Vector3 originPos;

    protected virtual void Awake()
    {
        rootRect = GetComponent<RectTransform>();
#if UNITY_EDITOR
        if (rootRect == null)
            Debug.LogError(gameObject.name + "is not setted rootRect");
#endif
        originPos = rootRect.localPosition;
    }

    public void Initialize(bool value)
    {
        isToggle = !value;
        Toggle(value);
    }

    public void Toggle(bool value)
    {
        if (isToggle == value) return;

        isToggle = value;
        rootRect.gameObject.SetActive(value);
        if (isToggle)
            rootRect.localPosition = Vector3.zero;
        else
            rootRect.localPosition = originPos;
    }
}
