using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressStore : MonoBehaviour
{
    //상점
    public Button[] FoodArray;
    public GameObject TextView; //텍스트 출력 장소

    [Space(10f)]
    public Text currentGold;

    [Space(10f)]
    public Text WhiteText;  //흰색 텍스트(기본)
    public Text RedText;    //빨간색 텍스트(증가)
    public Text BlueText;   //파란색 텍스트(감소)

    List<Text> tempList = new List<Text>();

    void Start()
    {
        //각 버튼에 구매 함수 추가
        for (int i = 0; i < FoodArray.Length; i++)
        {
            FoodArray[i].onClick.AddListener(BuyFood);
        }

        UpdateGoldUI();

    }

    //아이템 구매 함수
    public void BuyFood()
    {
        //리스트 비우기
        tempList.Clear();

        //각 데이터 테이블의 값 읽어온 string으로 string 리스트 생성하기
        //각 색깔의 텍스트 테스트
        Text whiteText = Instantiate(WhiteText);
        Text redText = Instantiate(RedText);
        Text blueText = Instantiate(BlueText);

        whiteText.text = "Food1 구매";
        tempList.Add(whiteText);
        blueText.text = "골드 -20000";
        tempList.Add(blueText);
        redText.text = "체력 10 증가";
        tempList.Add(redText);

        TextView.GetComponent<ProgressTyping>().SendText(tempList);

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
