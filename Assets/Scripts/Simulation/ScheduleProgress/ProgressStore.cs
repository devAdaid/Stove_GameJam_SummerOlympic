using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressStore : MonoBehaviour
{
    //상점
    public Button[] FoodArray;
    public int currentPage; //현재 페이지
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
        currentPage = 0;    //현재 페이지: 0

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
        
        //스탯, 돈 증감 적용하기

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

    public void LeftButton()
    {
        //왼쪽 버튼을 누르면
        if (currentPage != 0)
        {
            //현재 페이지가 0페이지가 아니라면
            currentPage--; //페이지 변경

            //상품 텍스트 변경(1~8번 상품)
        }
    }

    public void RightButton()
    {
        //오른쪽 버튼을 누르면
        if (currentPage != 1)
        {
            //현재 페이지가 1페이지가 아니라면
            currentPage++;    //페이지 변경

            //상품 텍스트 변경(9~16번 상품)
        }
    }
}
