using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreTest : MonoBehaviour
{
    //���� �׽�Ʈ��
    public Button[] FoodArray;
    public GameObject TextView;

    [Space(10f)]
    public Text currentGold;

    List<string> tempList = new List<string>();

    void Start()
    {
        for (int i = 0; i < FoodArray.Length; i++)
        {
            FoodArray[i].onClick.AddListener(BuyFood);
        }

        UpdateGoldUI();

    }

    public void BuyFood()
    {
        //����Ʈ ����
        tempList.Clear();

        //�� ������ ���̺��� �� �о�� string���� string ����Ʈ �����ϱ�
        tempList.Add("�׽�Ʈ��");
        tempList.Add("Food1 ����");
        tempList.Add("ü�� 10 ����");

        TextView.GetComponent<TypingTest>().SendText(tempList);

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
