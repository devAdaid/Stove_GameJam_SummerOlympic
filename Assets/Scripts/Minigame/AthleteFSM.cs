using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AthleteFSM : MonoBehaviour
{
    public State CurrentState { get { return currentState; } }
    public float CurrentSpeed { get { return currentSwimmingSpeed; } }

    [Header("References")]
    [SerializeField] protected Transform imageTransform;
    [SerializeField] protected Animator animator;
    [SerializeField] SwimStatManager statManager;
    [SerializeField] Transform frameTransform;
    [SerializeField] Transform waterImage;
    [SerializeField] float waterImageMax;
    [SerializeField] float waterImageMin;

    [Header("Read Only Values")]
    [ReadOnly] [SerializeField] protected State currentState;
    [ReadOnly] [SerializeField] float currentSwimmingSpeed;



    [HideInInspector] public float swimSpeedLerpSpeed;
    [HideInInspector] public float diveSwimDuration;
    [HideInInspector] public float maxSwimmingSpeed;
    [HideInInspector] public float bonusSwimmingSpeed;
    


    

    bool isDivingFailed = false;
    Vector3 startPosition;
    Vector3 moveDirection;
    public enum State
    {
        Ready, Diving, DiveSwim, DiveRecover ,Swimming, Finish
    }
    protected bool isNewState;

    protected void Awake()
    {
        currentState = State.Ready;
    }

    protected void Start()
    {
        startPosition = transform.position;
        moveDirection = new Vector3(2f,1f,0).normalized;
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
        for(int i=0;i<statManager.diveTimmingBounds.Length; i++)
        {
            if(timmingValue > statManager.diveTimmingBounds[i])
            {
                diveSwimDuration *= statManager.diveSwimDurationAlphas[i];
                break;
            }
        }
        if(timmingValue >= statManager.diveTimmingBounds[0])
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
        float originalHeight = frameTransform.localPosition.y;
        currentSwimmingSpeed = statManager.diveSpeed;
        while(eTime < statManager.diveDuration)
        {
            yield return null;
            eTime += Time.deltaTime;
            float yPos = Mathf.Lerp(originalHeight, statManager.diveDepth, TimeCurves.ExponentialMirrored(eTime / statManager.diveDuration));
            transform.position += moveDirection * currentSwimmingSpeed * Time.deltaTime;
            frameTransform.localPosition = new Vector3(0, yPos, 0);
        }
        frameTransform.localPosition = new Vector3(0, statManager.diveDepth, 0);
        ChangeState(State.DiveSwim);
    }
    IEnumerator DiveSwim()
    {
        float eTime = 0f;
        currentSwimmingSpeed = statManager.diveSwimSpeed;
        while(eTime < diveSwimDuration + statManager.bonusDiveSwimDuration)
        {
            yield return null;
            eTime += Time.deltaTime;
            transform.position += moveDirection * currentSwimmingSpeed * Time.deltaTime;
        }
        ChangeState(State.DiveRecover);
    }
    IEnumerator DiveRecover()
    {
        float eTime = 0f;
        float recoverSpeed = -frameTransform.localPosition.y / statManager.recoverDuration;
        currentSwimmingSpeed = statManager.diveSpeed;
        while (eTime < statManager.recoverDuration)
        {
            yield return null;
            eTime += Time.deltaTime;
            float t = eTime / statManager.recoverDuration;
            currentSwimmingSpeed = Mathf.Lerp(statManager.diveSpeed, statManager.defaultSwimmingSpeed, TimeCurves.Exponential(t));
            transform.position += moveDirection * currentSwimmingSpeed * Time.deltaTime;
            frameTransform.localPosition += new Vector3(0, recoverSpeed, 0) * Time.deltaTime;
        }
        frameTransform.localPosition = new Vector3(0, 0, 0);
        ChangeState(State.Swimming);
    }
    IEnumerator Swimming()
    {
        currentSwimmingSpeed = statManager.defaultSwimmingSpeed;
        while (!isNewState)
        {
            yield return null;
            currentSwimmingSpeed = Mathf.Lerp(currentSwimmingSpeed, statManager.defaultSwimmingSpeed, Time.deltaTime * swimSpeedLerpSpeed);
            if (currentSwimmingSpeed > maxSwimmingSpeed)
                currentSwimmingSpeed = maxSwimmingSpeed;
            transform.position += moveDirection * currentSwimmingSpeed * Time.deltaTime;
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
        if (currentState == State.DiveSwim)
        {
            diveSwimDuration += statManager.bonusDiveSwimDuration;
        }
        else if (currentState == State.Swimming)
        {
            currentSwimmingSpeed += bonusSwimmingSpeed;
        }
    }
    private void Update()
    {
        waterImage.localPosition = new Vector3(waterImage.localPosition.x, -frameTransform.localPosition.y + (waterImageMin+waterImageMax)/2, 0);
    }
}
