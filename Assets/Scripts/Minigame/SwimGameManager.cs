using System.Collections;
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


    [Header("Settings")]
    [SerializeField] int playerLane;
    [SerializeField] Transform leftCorner;
    [SerializeField] Transform rightCorner;
    [SerializeField] float readyWaitDuration;
    [SerializeField] float timmingBarDecreaseSpeed;

    enum State { Ready, Playing, Finish }

    bool isTimmerStopped = false;
    State currentState;
    int inputState = 0;
    private void Awake()
    {
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
        //다이빙 나오기 직전까지
        yield return new WaitForSeconds(readyWaitDuration);

        timmingBar.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);

        while(timmingBar.value > 0)
        {
            yield return null;

            timmingBar.value -= timmingBarDecreaseSpeed * Time.deltaTime;
            if(isTimmerStopped)
            {
                break;
            }
        }
        timmingBar.GetComponent<Animator>().SetTrigger("Close");

        for(int i=0; i<athletes.Length; i++)
        {
            if(i == playerLane)
                athletes[playerLane].DivePressed(timmingBar.value);
            else
                athletes[i].DivePressed(Random.Range(1, 80));
        }
        currentState = State.Playing;
        StartCoroutine(Playing());
    }
    IEnumerator Playing()
    {
        float timer = 0f;
        while(true)
        {
            //time
            SetTimeText(timer);
            timer += Time.deltaTime;
            //rank
            int rank = 0;
            for (int i = 0; i < athletes.Length; i++)
            {
                float realX = athletes[i].transform.position.x + (athletes[i].transform.position.y - athletes[playerLane].transform.position.y) * 2;
                if (realX >= athletes[playerLane].transform.position.x)
                    rank++;
            }
            SetRankText(rank);
            //speed
            SetSpeedText(athletes[playerLane].CurrentSpeed);

            yield return null;
            if(athletes[playerLane].CurrentState == AthleteFSM.State.Finish)
            {
                break;
            }
        }
        SetTimeText(timer);

        currentState = State.Finish;

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && currentState == State.Ready)
        {
            isTimmerStopped = true;
        }
        else if(currentState == State.Playing)
        {
            if(athletes[playerLane].CurrentState == AthleteFSM.State.Swimming)
            {
                if ((inputState == 0 || inputState == 2) && Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    athletes[playerLane].SwimButtonPressed();
                    inputState = 1;
                }
                else if ((inputState == 0 || inputState == 1) && Input.GetKeyDown(KeyCode.RightArrow))
                {
                    athletes[playerLane].SwimButtonPressed();   
                    inputState = 2;
                }
            }
            else 
            {
            if (Input.GetKeyDown(KeyCode.Space))
                {
                    athletes[playerLane].SwimButtonPressed();
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
}
