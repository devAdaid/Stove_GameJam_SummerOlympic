using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressStore : MonoBehaviour
{
    //����
    public Button[] FoodArray;
    public GameObject TextView; //�ؽ�Ʈ ��� ���

    [Space(10f)]
    public Text currentGold;

    [Space(10f)]
    public Text WhiteText;  //��� �ؽ�Ʈ(�⺻)
    public Text RedText;    //������ �ؽ�Ʈ(����)
    public Text BlueText;   //�Ķ��� �ؽ�Ʈ(����)

    List<Text> tempList = new List<Text>();

    void Start()
    {
        //�� ��ư�� ���� �Լ� �߰�
        for (int i = 0; i < FoodArray.Length; i++)
        {
            FoodArray[i].onClick.AddListener(BuyFood);
        }

        UpdateGoldUI();

    }

    //������ ���� �Լ�
    public void BuyFood()
    {
        //����Ʈ ����
        tempList.Clear();

        //�� ������ ���̺��� �� �о�� string���� string ����Ʈ �����ϱ�
        //�� ������ �ؽ�Ʈ �׽�Ʈ
        Text whiteText = Instantiate(WhiteText);
        Text redText = Instantiate(RedText);
        Text blueText = Instantiate(BlueText);

        whiteText.text = "Food1 ����";
        tempList.Add(whiteText);
        blueText.text = "��� -20000";
        tempList.Add(blueText);
        redText.text = "ü�� 10 ����";
        tempList.Add(redText);

        TextView.GetComponent<ProgressTyping>().SendText(tempList);

        UpdateGoldUI();
    }

    private void UpdateGoldUI()
    {
        currentGold.text = GetGold() + "��";

    }
    private string GetGold()
    {
        //���������� ��带 ������
        return $"{Simulation.I.Gold}";
    }
}
