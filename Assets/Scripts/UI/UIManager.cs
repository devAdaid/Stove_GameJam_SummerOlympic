using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : PersistentSingleton<UIManager>
{
    public UITitleView titleView;
    public UICharacterSettingView characterSettingView;

    public UISystemPanel systemPanel;

    protected override void Awake()
    {
        base.Awake();

        titleView.Initialize(true);
        characterSettingView.Initialize(false);

        systemPanel.Initialize(false);
    }

    private void AllToggleOff()
    {
        titleView.Toggle(false);
        characterSettingView.Toggle(false);

        systemPanel.Toggle(false);
    }



    public void SetCharacterSettingView()
    {
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
