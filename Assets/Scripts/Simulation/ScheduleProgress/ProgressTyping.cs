/*using System.Collections;
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


    //�ؽ�Ʈ ����Ʈ
    //�� ����Ʈ�� �ִ� �ؽ�Ʈ���� �������� ��µ�(ex. ���� �� ��, ������ �ϳ� ��)
    List<string> sendTextList = new List<string>();


    void Start()
    {
        isSkipped = false;  //��ŵ ���� �ʱ�ȭ
    }

    //�ؽ�Ʈ ��� ���� �Լ� -> �� �Լ��� �ؽ�Ʈ ����Ʈ�� �ְ� ȣ���ϸ� �ؽ�Ʈ�� ��µ�
    public void SendText(List<string> textList)
    {
        StartCoroutine(TypingText(textList));
    }

    //�ؽ�Ʈ ����Ʈ�� ����� �Լ�(������ )
    public void ProcessSchedule(List<StatChangeInfo> statChangeInfo, string scheduleName)
    {
        //������ ������Ű�� Ÿ���� ������ ������

        //����Ʈ �ʱ�ȭ
    }

    //�ؽ�Ʈ ����Ʈ�� �ؽ�Ʈ�� �����ϴ� �ڷ�ƾ �Լ�
    IEnumerator TypingText(List<string> textList)
    {
        for (int i = 0; i < textList.Count; i++)
        {
            //Text Content �ڽ� ������Ʈ�� �� �ؽ�Ʈ ����, Ÿ���� ����
            Text newText = Instantiate(TextPrefab);
            newText.transform.SetParent(TextContent.transform, false);
            yield return StartCoroutine(Typing(newText, textList[i], TypingSpeed));
        }

        isFinished = true;  //Ÿ���� ������ true�� ����
    }

    //Ÿ�����ϴ� �ڷ�ƾ �Լ�
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
*/