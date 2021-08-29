using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    public UITitleView titleView;
    public UICharacterSettingView characterSettingView;

    private void Awake()
    {
        titleView.Initialize(true);
        characterSettingView.Initialize(false);
    }

    private void AllToggleOff()
    {
        titleView.Toggle(false);
        characterSettingView.Toggle(false);
    }



    public void SetCharacterSettingView()
    {
        titleView.gameObject.SetActive(false);
        characterSettingView.gameObject.SetActive(true);
        AllToggleOff();
        characterSettingView.Toggle(true);
    }

    public void SetScheduleView()
    {
        AllToggleOff();
    }

    public void SetSystem()
    {

    }
}
