using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwimGameManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Slider timmingBar;
    [SerializeField] AthleteFSM[] athletes;
    [SerializeField] int playerLane;
    [SerializeField] Text rankText;
    [SerializeField] Text rankSubtext;


    [Header("Settings")]
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
}
