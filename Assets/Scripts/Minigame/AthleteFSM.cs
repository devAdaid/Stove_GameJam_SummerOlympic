using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AthleteFSM : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected Transform imageTransform;
    [SerializeField] protected Animator animator;
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] SwimGameManager gameManager;
    [SerializeField] Transform diveBottomPoint;
    [SerializeField] Transform diveRecoverPoint;

    [Header("Stats")]
    [SerializeField] float diveDuration;    //time untill reaching diveDepth
    [SerializeField] float recoverDuration;    //time untill recovering to recoverHeight

    [Header("Settings")]
    [SerializeField] float diveSpeed;
    [SerializeField] float defaultSwimmingSpeed;
    [SerializeField] float bonusRecoverDuration;
    [SerializeField] float bonusSwimmingSpeed;

    [Header("Diving Timing")]
    [SerializeField] float[] diveTimmingBounds;
    [SerializeField] float[] recoverDurationAlphas;
    [SerializeField] float[] diveSpeedAlphas;

    bool isDivingFailed = false;
    float bonusRecoverTotalDuration = 0f;
    float currentSwimmingSpeed;

    public enum State
    {
        Ready, Diving, DiveRecover ,Swimming, Finish
    }
    [ReadOnly] [SerializeField] protected State currentState;
    protected bool isNewState;


    protected void Awake()
    {
        currentState = State.Ready;
    }

    protected void Start()
    {
        StartCoroutine(FSMMain());
    }
    IEnumerator FSMMain()
    {
        while (true)
        {
            isNewState = false;
            yield return StartCoroutine(currentState.ToString());
        }
    }
    public void ChangeState(State state)
    {
        currentState = state;
        isNewState = true;
        animator.SetInteger("State", (int)currentState);
    }
    public void DivePressed(float timmingValue)
    {
        if (timmingValue == 0)
            timmingValue = 100;
        for(int i=0;i<diveTimmingBounds.Length; i++)
        {
            if(timmingValue >= diveTimmingBounds[i])
            {
                recoverDuration *= recoverDurationAlphas[i];
                diveSpeed *= diveSpeedAlphas[i];
                break;
            }
        }
        if(timmingValue >= diveTimmingBounds[0])
            isDivingFailed = true;
        ChangeState(State.Diving);

    }

    IEnumerator Ready()
    {
        yield return null;
    }
    IEnumerator Diving()
    {
        float eTime = 0f;
        Vector3 originalPos = transform.position;
        float diveDepth = diveBottomPoint.position.y - originalPos.y;
        while(eTime < diveDuration)
        {
            yield return null;
            eTime += Time.deltaTime;
            float yPos = Mathf.Lerp(0, diveDepth, TimeCurves.ExponentialMirrored(eTime / diveDuration));
            transform.position += new Vector3(diveSpeed * Time.deltaTime, 0, 0);
            transform.position = new Vector3(transform.position.x, originalPos.y + yPos, 0);
        }
        transform.position = new Vector3(transform.position.x, originalPos.y + diveDepth, 0);
        ChangeState(State.DiveRecover);
    }
    IEnumerator DiveRecover()
    {
        float swimmingSpeed = diveSpeed;
        float eTime = 0f;
        Vector3 originalPos = transform.position;
        float recoverHeight = diveRecoverPoint.position.y - originalPos.y;
        while (eTime < recoverDuration + bonusRecoverTotalDuration)
        {
            yield return null;
            eTime += Time.deltaTime;
            float t = eTime / recoverDuration;
            swimmingSpeed = Mathf.Lerp(diveSpeed, defaultSwimmingSpeed, TimeCurves.Exponential(t));
            transform.position += new Vector3(swimmingSpeed, recoverHeight / (recoverDuration+ bonusRecoverTotalDuration), 0) * Time.deltaTime;
        }
        transform.position = new Vector3(transform.position.x, originalPos.y + recoverHeight, 0);
        ChangeState(State.Swimming);
    }
    IEnumerator Swimming()
    {
        currentSwimmingSpeed = defaultSwimmingSpeed;
        while (!isNewState)
        {
            yield return null;
            currentSwimmingSpeed = Mathf.Lerp(currentSwimmingSpeed, defaultSwimmingSpeed, Time.deltaTime);
            transform.position += new Vector3(currentSwimmingSpeed, 0, 0) * Time.deltaTime;
        }
    }
    IEnumerator Finish()
    {
        yield return null;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("FinishLine"))
        {
            if (currentState == State.Swimming)
            {
                ChangeState(State.Finish);
            }
        }
    }
    public void SwimButtonPressed()
    {
        if (currentState == State.DiveRecover || currentState == State.Diving)
        {
            bonusRecoverTotalDuration += bonusRecoverDuration;
        }
        else if (currentState == State.Swimming)
        {
            currentSwimmingSpeed += bonusSwimmingSpeed;
        }
    }
}
