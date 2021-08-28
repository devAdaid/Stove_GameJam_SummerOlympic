using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MBTITest : MonoBehaviour
{
    public MBTI MBTI = new MBTI();
    public Text MBTIText;

    private void Awake()
    {
        UpdateMBTIUI();
    }

    public void SetM(bool isE)
    {
        var m = isE ? MBTIType_M.E : MBTIType_M.I;
        MBTI.SetM(m);
        UpdateMBTIUI();
    }

    public void SetB(bool isN)
    {
        var b = isN ? MBTIType_B.N : MBTIType_B.S;
        MBTI.SetB(b);
        UpdateMBTIUI();
    }

    public void SetT(bool isT)
    {
        var t = isT ? MBTIType_T.T : MBTIType_T.F;
        MBTI.SetT(t);
        UpdateMBTIUI();
    }

    public void SetI(bool isJ)
    {
        var i = isJ ? MBTIType_I.J : MBTIType_I.P;
        MBTI.SetI(i);
        UpdateMBTIUI();
    }

    private void UpdateMBTIUI()
    {
        MBTIText.text = MBTI.ToString();
    }
}
