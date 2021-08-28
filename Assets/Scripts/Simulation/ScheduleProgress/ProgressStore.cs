using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressStore : MonoBehaviour
{
    //����
    public Button[] ItemArray;
    public int currentPage; //���� ������
    public GameObject TextView; //�ؽ�Ʈ ��� ���

    [Space(10f)]
    public Text currentGold;

    [Space(10f)]
    public Text WhiteText;  //��� �ؽ�Ʈ(�⺻)
    public Text RedText;    //������ �ؽ�Ʈ(����)
    public Text BlueText;   //�Ķ��� �ؽ�Ʈ(����)

    List<Text> tempList = new List<Text>();
    List<StoreItemData> itemList = new List<StoreItemData>();
    StoreItemDatas StoreItemTest;
    void Awake()
    {
        //������ ����Ʈ
        //GameData itemData = new GameData();

        StoreItemTest = new StoreItemDatas();

        //itemList = itemData.StoreItem.Datas;
    }

    void Start()
    {
        currentPage = 0;    //���� ������: 0

        //�� ��ư�� ���� �Լ� �߰�
        for (int i = 0; i < ItemArray.Length; i++)
        {
            ItemArray[i].onClick.AddListener(BuyFood);
        }

        UpdateGoldUI();

        //StoreItemData currentItem = returnOneOfStoreItem();
        Debug.Log(StoreItemTest.Datas[0].Item);
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
    

    //������ ��ȣ�� ���� ������ �ϳ��� ���� ��ȯ�ϱ�
    private StoreItemData returnOneOfStoreItem(int index)
    {
        return itemList[index];
    }

    //������ ��ȣ�� ���� �����۵��� ���� �ؽ�Ʈ�� ����ϱ�(���� ��ġ��)
    private void printOneOfStoreItem(int index)
    {
        //StoreItemData currentItem = new StoreItemData();
        StoreItemData currentItem = returnOneOfStoreItem(index);

        string result;
        result = currentItem.Item + "\n";  //������ �̸�

        string[] changedStatName = new string[5];
        int[] changedStatValue = new int[5];
        int count = 0;

        if (currentItem.Endurance > 0)
        {
            changedStatName[count] += "������ ";
            changedStatValue[count] = currentItem.Endurance;
            count++;
        }
        if (currentItem.Quickness > 0)
        {
            changedStatName[count] += "���߷� ";
            changedStatValue[count] = currentItem.Quickness;
            count++;
        }
        if (currentItem.Strength > 0)
        {
            changedStatName[count] += "�ٷ� ";
            changedStatValue[count] = currentItem.Strength;
            count++;
        }
        if (currentItem.Flexibility > 0)
        {
            changedStatName[count] += "������ ";
            changedStatValue[count] = currentItem.Flexibility;
            count++;
        }
        if (currentItem.Stamina > 0)
        {
            changedStatName[count] += "ü�� ";
            changedStatValue[count] = currentItem.Stamina;
            count++;
        }

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
