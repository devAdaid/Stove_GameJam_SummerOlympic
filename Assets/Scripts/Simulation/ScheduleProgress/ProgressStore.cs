using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressStore : MonoBehaviour
{
    //����
    public Button[] FoodArray;
    public int currentPage; //���� ������
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
        currentPage = 0;    //���� ������: 0

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
        
        //����, �� ���� �����ϱ�

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

    public void LeftButton()
    {
        //���� ��ư�� ������
        if (currentPage != 0)
        {
            //���� �������� 0�������� �ƴ϶��
            currentPage--; //������ ����

            //��ǰ �ؽ�Ʈ ����(1~8�� ��ǰ)
        }
    }

    public void RightButton()
    {
        //������ ��ư�� ������
        if (currentPage != 1)
        {
            //���� �������� 1�������� �ƴ϶��
            currentPage++;    //������ ����

            //��ǰ �ؽ�Ʈ ����(9~16�� ��ǰ)
        }
    }
}
