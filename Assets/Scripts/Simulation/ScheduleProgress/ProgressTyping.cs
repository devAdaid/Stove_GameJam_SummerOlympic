using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//일정 진행 과정을 타이핑하는 클래스
public class ProgressTyping : MonoBehaviour
{
    [Header("Typing Objects")]
    public GameObject TextContent;  //텍스트를 UI를 생성할 위치
    public Text WhiteText;  //흰색 텍스트(기본)
    public Text RedText;    //빨간색 텍스트(증가)
    public Text BlueText;   //파란색 텍스트(감소)

    [Header("Typing Speed")]
    public float TypingSpeed = 0.1f;    //타이핑 속도
    public bool isSkipped;  //스킵 버튼을 눌렀는지 여부
    public bool isFinished;

    //텍스트 리스트
    //이 리스트에 있는 텍스트들이 묶음으로 출력됨(ex. 일정 한 주 설명, 아이템 하나 설명 등)
    List<Text> sendTextList = new List<Text>();


    void Start()
    {
        isSkipped = isFinished = false;  //스킵 여부 초기화
    }

    //텍스트 출력 실행 함수 -> 이 함수에 텍스트 리스트를 넣고 호출하면 텍스트가 출력됨
    public void SendText(List<Text> textList)
    {
        StartCoroutine(TypingText(textList));
    }


    public void ProcessSchedule(ProcessScheduleRequestData requestData, string scheduleName, int day)
    {
        //일정을 증가시키고 타이핑 연출을 보여줌
        //각 색깔의 텍스트
        Text whiteText = Instantiate(WhiteText);
        Text redText = Instantiate(RedText);
        Text blueText = Instantiate(BlueText);

        //리스트 초기화
        sendTextList.Clear();

        //텍스트 작성
        whiteText.text = scheduleName + "을 시작했다.";
        sendTextList.Add(whiteText); //~~일정을 시작했다.

        if (requestData.GoldDiff >= 0)
        {
            redText.text = "골드 " + requestData.GoldDiff;
            sendTextList.Add(redText); //골드 +금액
        }
        else
        {
            blueText.text = "골드 " + requestData.GoldDiff;
            sendTextList.Add(blueText); //골드 +금액
        }

        List<StatChangedInfo> changedStats = requestData.StatChangedInfo;   //일정 리스트
        //일정 하나의 스탯들 증감 저장
        for (int i = 0; i < changedStats.Count; i++)
        {
            string tempString = changedStats[i].StatType + " " + changedStats[i].DiffValue;

            if (changedStats[i].IsIncreased)
            {

                redText.text = tempString;
                sendTextList.Add(redText);
            }
            else
            {
                blueText.text = tempString;
                sendTextList.Add(blueText);
            }
        }

        whiteText.text = scheduleName + "을 마쳤다.";
        sendTextList.Add(whiteText); //~~일정을 마쳤다.


        //저장한 텍스트들 출력(타이핑 연출)
        SendText(sendTextList);

        //빈 메소드
        NextSchedule();
    }

    //아영님이 요청하신 빈 메소드
    public void NextSchedule()
    {

    }

    //텍스트 리스트의 텍스트를 타이핑하는 코루틴 함수
    IEnumerator TypingText(List<Text> textList)
    {
        for (int i = 0; i < textList.Count; i++)
        {
            //Text Content 자식 오브젝트에 새 텍스트 생성, 타이핑 연출
            Text newText = Instantiate(textList[i]);
            newText.transform.SetParent(TextContent.transform, false);
            yield return StartCoroutine(Typing(newText, textList[i].text, TypingSpeed));
        }

        isFinished = true;  //타이핑 끝나면 true로 설정
    }

    //한 문장을 타이핑하는 코루틴 함수
    IEnumerator Typing(Text typingText, string message, float speed)
    {
        for (int i = 0; i < message.Length; i++)
        {
            typingText.text = message.Substring(0, i + 1);
            yield return new WaitForSeconds(speed);
        }
    }

    //스킵 버튼 온오프로 타이핑 속도 변경
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
}
