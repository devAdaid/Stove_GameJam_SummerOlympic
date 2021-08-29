using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchedueProgressRoot : MonoSingleton<SchedueProgressRoot>
{
    public GameObject Root;

    public ProgressTyping Progress;

    public void SetActive(bool flag)
    {
        Root.SetActive(flag);
    }
}
