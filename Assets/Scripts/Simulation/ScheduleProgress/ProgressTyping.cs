using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//���� ���� ������ Ÿ�����ϴ� Ŭ����
public class ProgressTyping : MonoBehaviour
{
    [Header("Typing Objects")]
    public GameObject TextContent;  //�ؽ�Ʈ�� UI�� ������ ��ġ
    public Text WhiteText;  //��� �ؽ�Ʈ(�⺻)
    public Text RedText;    //������ �ؽ�Ʈ(����)
    public Text BlueText;   //�Ķ��� �ؽ�Ʈ(����)

    [Header("Typing Speed")]
    public float TypingSpeed = 0.1f;    //Ÿ���� �ӵ�
    public bool isSkipped;  //��ŵ ��ư�� �������� ����
    public bool isFinished;

    //�ؽ�Ʈ ����Ʈ
    //�� ����Ʈ�� �ִ� �ؽ�Ʈ���� �������� ��µ�(ex. ���� �� �� ����, ������ �ϳ� ���� ��)
    List<Text> sendTextList = new List<Text>();


    void Start()
    {
        isSkipped = isFinished = false;  //��ŵ ���� �ʱ�ȭ
    }

    //�ؽ�Ʈ ��� ���� �Լ� -> �� �Լ��� �ؽ�Ʈ ����Ʈ�� �ְ� ȣ���ϸ� �ؽ�Ʈ�� ��µ�
    public void SendText(List<Text> textList)
    {
        StartCoroutine(TypingText(textList));
    }


    public void ProcessSchedule(ProcessScheduleRequestData requestData, string scheduleName, int day)
    {
        //������ ������Ű�� Ÿ���� ������ ������
        //�� ������ �ؽ�Ʈ
        Text whiteText = Instantiate(WhiteText);
        Text redText = Instantiate(RedText);
        Text blueText = Instantiate(BlueText);

        //����Ʈ �ʱ�ȭ
        sendTextList.Clear();

        //�ؽ�Ʈ �ۼ�
        whiteText.text = scheduleName + "�� �����ߴ�.";
        sendTextList.Add(whiteText); //~~������ �����ߴ�.

        if (requestData.GoldDiff >= 0)
        {
            redText.text = "��� " + requestData.GoldDiff;
            sendTextList.Add(redText); //��� +�ݾ�
        }
        else
        {
            blueText.text = "��� " + requestData.GoldDiff;
            sendTextList.Add(blueText); //��� +�ݾ�
        }

        List<StatChangedInfo> changedStats = requestData.StatChangedInfo;   //���� ����Ʈ
        //���� �ϳ��� ���ȵ� ���� ����
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

        whiteText.text = scheduleName + "�� ���ƴ�.";
        sendTextList.Add(whiteText); //~~������ ���ƴ�.


        //������ �ؽ�Ʈ�� ���(Ÿ���� ����)
        SendText(sendTextList);

        //�� �޼ҵ�
        NextSchedule();
    }

    //�ƿ����� ��û�Ͻ� �� �޼ҵ�
    public void NextSchedule()
    {

    }

    //�ؽ�Ʈ ����Ʈ�� �ؽ�Ʈ�� Ÿ�����ϴ� �ڷ�ƾ �Լ�
    IEnumerator TypingText(List<Text> textList)
    {
        for (int i = 0; i < textList.Count; i++)
        {
            //Text Content �ڽ� ������Ʈ�� �� �ؽ�Ʈ ����, Ÿ���� ����
            Text newText = Instantiate(textList[i]);
            newText.transform.SetParent(TextContent.transform, false);
            yield return StartCoroutine(Typing(newText, textList[i].text, TypingSpeed));
        }

        isFinished = true;  //Ÿ���� ������ true�� ����
    }

    //�� ������ Ÿ�����ϴ� �ڷ�ƾ �Լ�
    IEnumerator Typing(Text typingText, string message, float speed)
    {
        for (int i = 0; i < message.Length; i++)
        {
            typingText.text = message.Substring(0, i + 1);
            yield return new WaitForSeconds(speed);
        }
    }

    //��ŵ ��ư �¿����� Ÿ���� �ӵ� ����
    public void SkipOnOff()
    {
        if (!isSkipped)  //false��� true�� ���� == ��ŵ
        {
            isSkipped = true;
            TypingSpeed = 0.02f;
        }
        else    //true��� false�� ���� == ��ŵ ����
        {
            isSkipped = false;
            TypingSpeed = 0.1f;
        }
    }

    //�޽��� ��� ����
    public void DeleteText()
    {
        if (isFinished) //Ÿ������ �����ٸ�
        {
            var childText = TextContent.GetComponentsInChildren<Transform>();  //Text Content�� ������ �ؽ�Ʈ��

            foreach (var iter in childText)
            {
                //Text Content�� �������� ����
                if (iter != TextContent.transform)
                {
                    Destroy(iter.gameObject);
                }
            }
        }
    }
}
