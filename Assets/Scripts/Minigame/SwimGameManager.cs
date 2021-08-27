using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwimGameManager : MonoBehaviour
{
    [Header("References")]
    public Slider timmingBar;
    [SerializeField] AthleteFSM player;
    
    [Header("Settings")]
    [SerializeField] float readyWaitDuration;
    [SerializeField] float timmingBarDecreaseSpeed;

    enum State { Ready, Playing, Finish }

    bool isTimmerStopped = false;
    State currentState;
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
        player.DivePressed(timmingBar.value);
        timmingBar.GetComponent<Animator>().SetTrigger("Close");
        //change button "stop timmer" -> "swim"
        //start all athletes



        currentState = State.Playing;
        StartCoroutine(Playing());
    }
    IEnumerator Playing()
    {
        yield return null;
    }
    public void OnMainButtonPressed()
    {
        if (currentState == State.Ready)
            isTimmerStopped = true;
        else if (currentState == State.Playing)
            player.SwimButtonPressed();
    }
}
