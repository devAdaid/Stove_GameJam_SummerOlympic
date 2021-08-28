using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreTest : MonoBehaviour
{
    //상점 테스트용
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
        //리스트 비우기
        tempList.Clear();

        //각 데이터 테이블의 값 읽어온 string으로 string 리스트 생성하기
        tempList.Add("테스트중");
        tempList.Add("Food1 구매");
        tempList.Add("체력 10 증가");

        TextView.GetComponent<TypingTest>().SendText(tempList);

        UpdateGoldUI();
    }

    private void UpdateGoldUI()
    {
        currentGold.text = GetGold() + "원";

    }
    private string GetGold()
    {
        //수영선수의 골드를 가져옴
        return $"{Simulation.I.Gold}";
    }
}
