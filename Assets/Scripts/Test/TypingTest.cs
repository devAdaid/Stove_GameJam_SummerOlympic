using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Ÿ���� �׽�Ʈ
public class TypingTest : MonoBehaviour
{
    [Header("Typing Objects")]
    public GameObject TextContent;  //�ؽ�Ʈ�� UI�� ������ ��ġ
    public Text TextPrefab;

    [Header("Typing Speed")]
    public float TypingSpeed = 0.1f;
    public bool isSkipped;

    [Space(10f)]
    public bool isFinished; //Ÿ������ �� �������� ����

    [Header("Store")]
    public GameObject storeView;

    List<string> textList = new List<string>(); //�ؽ�Ʈ ����Ʈ

    //���ڻ� ����: ������ �κ��� <color=�����ڵ�>�� </color> ���̿� �ֱ�

    void Start()
    {
        isSkipped = isFinished = false;
        storeView.SetActive(false); //���� ��Ȱ��ȭ

        //�ӽ� �ؽ�Ʈ ����Ʈ �߰�. 
        textList.Add("���⼭ �޽��� ���");
        textList.Add("Ÿ���� ȿ�� Ȯ��");
        textList.Add("� Ȱ���� �ߴ��� Ȯ���ϰ�");
        textList.Add("������ ���̺��� �޽����� ������ ����");
        textList.Add("�ö󰡰� ������ ��ġ�� <color=#FF0000>���ڻ� �����ؼ�</color> ǥ���� ����");
        textList.Add("���ڻ��� ���߿� ����Ǵ� ���� �����ؾ���");



        StartCoroutine(TypingText());
    }

    //�ؽ�Ʈ ����Ʈ�� �ؽ�Ʈ�� �����ϴ� �ڷ�ƾ �Լ�
    IEnumerator TypingText()
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

    //��ŵ ��ư ������ ��� �ӵ� ������ -> Ÿ���� �ӵ� ����
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

    public void StoreOpen()
    {
        //���� �ؽ�Ʈ�� ������ ������ ����
        storeView.SetActive(true); //���� Ȱ��ȭ
    }

    public void StoreClose()
    {
        //���� �ݴ� �Լ�
        storeView.SetActive(false); //���� ��Ȱ��ȭ
    }

    public void ScheduleFinish()
    {
        //������ ��ư�� ������ ���� ����
        StoreClose();
        DeleteText();   //�ؽ�Ʈ ��� ����
    }
}
