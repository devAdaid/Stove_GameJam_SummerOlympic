using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwimGameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Slider timmingBar;
    [SerializeField] Text rankText;
    [SerializeField] Text rankSubtext;
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

    private void Start()
    {
        StartCoroutine(Ready());
        currentState = State.Ready;
    }
    IEnumerator Ready()
    {
        rightCorner.position = new Vector3(rightCorner.position.x, leftCorner.position.y - (rightCorner.position.x - leftCorner.position.x) / 2, 0);
        for (int i=0; i< athletes.Length; i++)
        {
            athletes[i].transform.position = leftCorner.position + (rightCorner.position - leftCorner.position) * (i+1) / (athletes .Length+ 1);
        }
        //���̺� ������ ��������
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
        //change button "stop timmer" -> "swim"
        //start all athletes
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
        yield return null;
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
        int rank = 0;
        for(int i=0; i<athletes.Length; i++)
        {
            float realX = athletes[i].transform.position.x + (athletes[i].transform.position.y - athletes[playerLane].transform.position.y) * 2;
            if (realX >= athletes[playerLane].transform.position.x)
                rank++;
        }
        SetRankText(rank);
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
    public AthleteFSM GetPlayer()
    {
        return athletes[playerLane];
    }
}