using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//타이핑 테스트
public class TypingTest : MonoBehaviour
{
    [Header("Typing Objects")]
    public GameObject TextContent;  //텍스트를 UI를 생성할 위치
    public Text TextPrefab;

    [Header("Typing Speed")]
    public float TypingSpeed = 0.1f;
    public bool isSkipped;

    [Space(10f)]
    public bool isFinished; //타이핑이 다 끝났는지 여부

    [Header("Store")]
    public GameObject storeView;

    List<string> sendTextList = new List<string>(); //텍스트 리스트

    //글자색 변경: 변경할 부분을 <color=색상코드>와 </color> 사이에 넣기

    void Start()
    {
        isSkipped = isFinished = false;
        storeView.SetActive(false); //상점 비활성화

        //임시 텍스트 리스트 추가. 
        sendTextList.Add("여기서 메시지 출력");
        sendTextList.Add("타이핑 효과 확인");
        sendTextList.Add("어떤 활동을 했는지 확인하고");
        sendTextList.Add("데이터 테이블에서 메시지를 가져올 예정");
        sendTextList.Add("올라가고 내려간 수치는 <color=#FF0000>글자색 변경해서</color> 표현할 예정");
        sendTextList.Add("글자색이 나중에 변경되는 것은 수정해야함");



        SendText(sendTextList);
    }

    public void SendText(List<string> textList)
    {
        StartCoroutine(TypingText(textList));
    }

    //텍스트 리스트의 텍스트를 생성하는 코루틴 함수
    IEnumerator TypingText(List<string> textList)
    {
        for (int i = 0; i < textList.Count; i++)
        {
            //Text Content 자식 오브젝트에 새 텍스트 생성, 타이핑 연출
            Text newText = Instantiate(TextPrefab);
            newText.transform.SetParent(TextContent.transform, false);
            yield return StartCoroutine(Typing(newText, textList[i], TypingSpeed));
        }

        isFinished = true;  //타이핑 끝나면 true로 설정
    }

    //타이핑하는 코루틴 함수
    IEnumerator Typing(Text typingText, string message, float speed)
    {
        for (int i = 0; i < message.Length; i++)
        {
            typingText.text = message.Substring(0, i + 1);
            yield return new WaitForSeconds(speed);
        }
    }

    //스킵 버튼 누르면 출력 속도 빨라짐 -> 타이핑 속도 변경
    public void SkipOnOff()
    {
        if (!isSkipped)  //false라면 true로 변경 == 스킵
        {
            isSkipped = true;
            TypingSpeed = 0.02f;
        }
        else    //true라면 false로 변경 == 스킵 해제
        {
            isSkipped = false;
            TypingSpeed = 0.1f;
        }
    }

    //메시지 모두 삭제
    public void DeleteText()
    {
        if (isFinished) //타이핑이 끝났다면
        {
            var childText = TextContent.GetComponentsInChildren<Transform>();  //Text Content와 생성한 텍스트들

            foreach (var iter in childText)
            {
                //Text Content는 삭제하지 않음
                if (iter != TextContent.transform)
                {
                    Destroy(iter.gameObject);
                }
            }
        }
    }

    public void StoreOpen()
    {
        //월말 텍스트가 끝나면 상점이 열림
        storeView.SetActive(true); //상점 활성화
    }

    public void StoreClose()
    {
        //상점 닫는 함수
        storeView.SetActive(false); //상점 비활성화
    }

    public void ScheduleFinish()
    {
        //끝내기 버튼을 누르면 일정 종료
        StoreClose();
        DeleteText();   //텍스트 모두 삭제
    }
}
