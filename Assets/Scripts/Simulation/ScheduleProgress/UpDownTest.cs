using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpDownTest : MonoBehaviour
{
    //스탯이 오르면 UP 활성화, 텍스트 수정
    //스탯이 내려가면 Down 활성화, 텍스트 수정(체력만 Down 있음)
    //UP Down 세모 표시 ▲, ▼로 설정함(임시)

    public Text StaminaUp;
    public Text StaminaDown;

    public Text StrengthUp;
    public Text QuicknessUp;
    public Text EnduranceUp;
    public Text FlexibilityUp;

    public void ShowStaminaUp(int value)
    {
        //체력 증가치를 입력하면 텍스트 수정되어 활성화
        StaminaUp.text = "▲ " + value;
        StaminaUp.gameObject.SetActive(true);
    }

    public void ShowStaminaDown(int value)
    {
        //체력 감소치를 입력하면 텍스트 수정되어 활성화
        StaminaDown.text = "▼ " + value;
        StaminaDown.gameObject.SetActive(true);
    }

    public void ShowStrengthUp(int value)
    {
        //근력 증가치를 입력하면 텍스트 수정되어 활성화
        StrengthUp.text = "▲ " + value;
        StrengthUp.gameObject.SetActive(true);
    }

    public void ShowQuicknessUp(int value)
    {
        //순발력 증가치를 입력하면 텍스트 수정되어 활성화
        QuicknessUp.text = "▲ " + value;
        QuicknessUp.gameObject.SetActive(true);
    }

    public void ShowEnduranceUp(int value)
    {
        //지구력 증가치를 입력하면 텍스트 수정되어 활성화
        EnduranceUp.text = "▲ " + value;
        EnduranceUp.gameObject.SetActive(true);
    }

    public void ShowFlexibilityUp(int value)
    {
        //유연성 증가치를 입력하면 텍스트 수정되어 활성화
        FlexibilityUp.text = "▲ " + value;
        FlexibilityUp.gameObject.SetActive(true);
    }
}
