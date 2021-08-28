using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpDownTest : MonoBehaviour
{
    //������ ������ UP Ȱ��ȭ, �ؽ�Ʈ ����
    //������ �������� Down Ȱ��ȭ, �ؽ�Ʈ ����(ü�¸� Down ����)
    //UP Down ���� ǥ�� ��, ��� ������(�ӽ�)

    public Text StaminaUp;
    public Text StaminaDown;

    public Text StrengthUp;
    public Text QuicknessUp;
    public Text EnduranceUp;
    public Text FlexibilityUp;

    public void ShowStaminaUp(int value)
    {
        //ü�� ����ġ�� �Է��ϸ� �ؽ�Ʈ �����Ǿ� Ȱ��ȭ
        StaminaUp.text = "�� " + value;
        StaminaUp.gameObject.SetActive(true);
    }

    public void ShowStaminaDown(int value)
    {
        //ü�� ����ġ�� �Է��ϸ� �ؽ�Ʈ �����Ǿ� Ȱ��ȭ
        StaminaDown.text = "�� " + value;
        StaminaDown.gameObject.SetActive(true);
    }

    public void ShowStrengthUp(int value)
    {
        //�ٷ� ����ġ�� �Է��ϸ� �ؽ�Ʈ �����Ǿ� Ȱ��ȭ
        StrengthUp.text = "�� " + value;
        StrengthUp.gameObject.SetActive(true);
    }

    public void ShowQuicknessUp(int value)
    {
        //���߷� ����ġ�� �Է��ϸ� �ؽ�Ʈ �����Ǿ� Ȱ��ȭ
        QuicknessUp.text = "�� " + value;
        QuicknessUp.gameObject.SetActive(true);
    }

    public void ShowEnduranceUp(int value)
    {
        //������ ����ġ�� �Է��ϸ� �ؽ�Ʈ �����Ǿ� Ȱ��ȭ
        EnduranceUp.text = "�� " + value;
        EnduranceUp.gameObject.SetActive(true);
    }

    public void ShowFlexibilityUp(int value)
    {
        //������ ����ġ�� �Է��ϸ� �ؽ�Ʈ �����Ǿ� Ȱ��ȭ
        FlexibilityUp.text = "�� " + value;
        FlexibilityUp.gameObject.SetActive(true);
    }
}
