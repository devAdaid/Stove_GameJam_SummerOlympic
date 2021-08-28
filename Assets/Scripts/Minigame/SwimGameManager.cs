﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwimGameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] SwimStatManager statManager;
    [SerializeField] Slider timmingBar;
    [SerializeField] Text rankText;
    [SerializeField] Text rankSubtext;
    [SerializeField] Text speedText;
    [SerializeField] Text timeText;
    [SerializeField] AthleteFSM[] athletes;
    [SerializeField] Text howToPlayText;
    [SerializeField] ResultBoard resultBoard;
    [SerializeField] CountDown countDown;
    [SerializeField] Animator mainUIs;


    [Header("Settings")]
    [SerializeField] int playerLane;
    [SerializeField] Transform leftCorner;
    [SerializeField] Transform rightCorner;
    [SerializeField] float readyWaitDuration;
    [SerializeField] float valuePerSpaceHit;
    [SerializeField] float howToPlayShowDelay;


    List<AthleteFSM> finishedOrder = new List<AthleteFSM>();
    bool[] isFinished;
    enum State { Ready, Playing, Finish }

    State currentState;
    int inputState = 0;
    float eTimeSinceLastInput = 0f;
    bool isGettingSpaceInput = false;
    private void Awake()
    {
        isFinished = new bool[athletes.Length];
        for (int i = 0; i < isFinished.Length; i++)
            isFinished[i] = false;
        rightCorner.position = new Vector3(rightCorner.position.x, leftCorner.position.y - (rightCorner.position.x - leftCorner.position.x) / 2, 0);
        for (int i = 0; i < athletes.Length; i++)
        {
            athletes[i].transform.position = leftCorner.position + (rightCorner.position - leftCorner.position) * (i + 1) / (athletes.Length + 1);
        }
        statManager.SetStats(athletes, playerLane);

    }
    private void Start()
    {
        StartCoroutine(Ready());
        currentState = State.Ready;
    }
    IEnumerator Ready()
    {
        yield return new WaitForSeconds(readyWaitDuration);
        timmingBar.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        yield return StartCoroutine(ShowHowToPlayAndWait("스페이스바를 연타하세요!", true));
        
        isGettingSpaceInput = true;
        yield return StartCoroutine(countDown.StartCountDown(1));
        mainUIs.SetTrigger("Open");
        isGettingSpaceInput = false;

        timmingBar.GetComponent<Animator>().SetTrigger("Close");

        for(int i=0; i<athletes.Length; i++)
        {
            if(i == playerLane)
                athletes[playerLane].StartDive(timmingBar.value);
            else
                athletes[i].StartDive(athletes[i].diveStat);
        }
        currentState = State.Playing;
        StartCoroutine(Playing());
    }
    IEnumerator Playing()
    {
        float timer = 0f;
        eTimeSinceLastInput = 0f;
        while (true)
        {
            //time
            SetTimeText(timer);
            timer += Time.deltaTime;
            eTimeSinceLastInput += Time.deltaTime;

            //rank
            int rank = 0;
            for (int i = 0; i < athletes.Length; i++)
            {
                float realX = athletes[i].transform.position.x + (athletes[i].transform.position.y - athletes[playerLane].transform.position.y) * 2;
                if (realX >= athletes[playerLane].transform.position.x)
                    rank++;
                if(athletes[i].CurrentState == AthleteFSM.State.Finish && !isFinished[i])
                {
                    isFinished[i] = true;
                    athletes[i].finishedTime = timer;
                    finishedOrder.Add(athletes[i]);
                }
            }
            SetRankText(rank);
            //speed
            SetSpeedText(athletes[playerLane].CurrentSpeed);

            yield return null;
            if(athletes[playerLane].CurrentState == AthleteFSM.State.Finish)
            {
                //���� ������ ����Ʈ
                break;
            }
            if (eTimeSinceLastInput >= howToPlayShowDelay)
            {
                eTimeSinceLastInput = 0f;
                yield return StartCoroutine(ShowHowToPlayAndWait("좌우 방향키를 연타하세요!", false));
            }
        }
        SetTimeText(timer);


        Time.timeScale = 2;
        while(finishedOrder.Count != athletes.Length)
        {
            yield return null;
            timer += Time.deltaTime;
            for (int i = 0; i < athletes.Length; i++)
            {
                if (athletes[i].CurrentState == AthleteFSM.State.Finish && !isFinished[i])
                {
                    isFinished[i] = true;
                    athletes[i].finishedTime = timer;
                    finishedOrder.Add(athletes[i]);
                }
            }

        }
        Time.timeScale = 1;

        currentState = State.Finish;
        StartCoroutine(Finish());
    }
    IEnumerator Finish()
    {
        mainUIs.SetTrigger("Close");
        yield return new WaitForSeconds(2);
        resultBoard.SetValues(finishedOrder);
        resultBoard.gameObject.SetActive(true);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGettingSpaceInput)
        {
            timmingBar.value += valuePerSpaceHit;
        }
        else if(currentState == State.Playing)
        {
            if(athletes[playerLane].CurrentState == AthleteFSM.State.Swimming)
            {
                if ((inputState == 0 || inputState == 2) && Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    athletes[playerLane].SwimButtonPressed();
                    inputState = 1;
                    eTimeSinceLastInput = 0f;
                }
                else if ((inputState == 0 || inputState == 1) && Input.GetKeyDown(KeyCode.RightArrow))
                {
                    athletes[playerLane].SwimButtonPressed();   
                    inputState = 2;
                    eTimeSinceLastInput = 0f;
                }
            }
            else 
            {
            if (Input.GetKeyDown(KeyCode.Space))
                {
                    athletes[playerLane].SwimButtonPressed();

                    eTimeSinceLastInput = 0f;
                }
            }
        }
    }
    void SetRankText(int rank)
    {
        rankText.text = rank.ToString();
        if (rank == 1)
            rankSubtext.text = "st";
        else if (rank == 2)
            rankSubtext.text = "nd";
        else if (rank == 3)
            rankSubtext.text = "rd";
        else
            rankSubtext.text = "th";
    }
    void SetSpeedText(float speed)
    {
        speedText.text = string.Format("{0:0.0}", speed);
    }
    void SetTimeText(float eTime)
    {
        timeText.text = ((int)eTime/60).ToString().PadLeft(2,'0') + ":"
            + string.Format("{0:00.00}", (eTime % 60));
    }
    public AthleteFSM GetPlayer()
    {
        return athletes[playerLane];
    }
    IEnumerator ShowHowToPlayAndWait(string text, bool isDive)
    {
        howToPlayText.text = text;
        howToPlayText.transform.parent.gameObject.SetActive(true);
        Time.timeScale = 0f;
        while(true)
        {
            if(isDive)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                    break;
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    inputState = 1;
                    break;
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    inputState = 2;
                    break;
                }
            }
        yield return null;
        }
        Time.timeScale = 1;
        howToPlayText.transform.parent.gameObject.SetActive(false);
    }
}
