using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UICharacterSettingView : UIView
{
    public InputField playerNameInputField;

    public MBTI MBTI = new MBTI();


    private string originPlayerName;

    protected override void Awake()
    {
        base.Awake();
        originPlayerName = playerNameInputField.text;
    }


    public void SetM(bool isE)
    {
        var m = isE ? MBTIType_M.E : MBTIType_M.I;
        MBTI.SetM(m);
    }

    public void SetB(bool isS)
    {
        var b = isS ? MBTIType_B.S : MBTIType_B.N;
        MBTI.SetB(b);
    }

    public void SetT(bool isF)
    {
        var t = isF ? MBTIType_T.F : MBTIType_T.T;
        MBTI.SetT(t);
    }

    public void SetI(bool isP)
    {
        var i = isP ? MBTIType_I.P : MBTIType_I.J;
        MBTI.SetI(i);
    }

    public bool IsCompleteInput() =>
        (playerNameInputField.text != string.Empty) && MBTI.IsValid();


    public void CompleteSetting()
    {
        if (IsCompleteInput())
        {
            var stats = GameData.I.MBTI.GetDefaultStats(MBTI);
            Simulation.I.SetBaseStat(stats);
            Simulation.I.Swimmer.SetName(playerNameInputField.text);
            SceneManager.LoadScene("2_Schedule");
        }
    }

}
