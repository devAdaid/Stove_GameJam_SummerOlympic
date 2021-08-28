using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AthleteFSM : MonoBehaviour
{
    public State CurrentState { get { return currentState; } }

    [Header("References")]
    [SerializeField] protected Transform imageTransform;
    [SerializeField] protected Animator animator;
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] SwimGameManager gameManager;
    [SerializeField] SwimEffectManager effectManager;
    [SerializeField] Transform frameTransform;
    [SerializeField] Transform waterImage;
    [SerializeField] float waterImageMax;
    [SerializeField] float waterImageMin;

    [Header("Read Only Values")]
    [ReadOnly] [SerializeField] protected State currentState;
    [ReadOnly] [SerializeField] float currentSwimmingSpeed;
    [ReadOnly] [SerializeField] float bonusRecoverTotalDuration = 0f;

    [Header("Stats")]
    [SerializeField] float diveDuration;    //time untill reaching diveDepth
    [SerializeField] float recoverDuration;    //time untill recovering to recoverHeight

    [Header("Settings")]
    [SerializeField] float diveSpeed;
    [SerializeField] float diveDepth;
    [SerializeField] float defaultSwimmingSpeed;
    [SerializeField] float bonusRecoverDuration;
    [SerializeField] float bonusSwimmingSpeed;

    [Header("Diving Timing")]
    [SerializeField] float[] diveTimmingBounds;
    [SerializeField] float[] recoverDurationAlphas;
    [SerializeField] float[] diveSpeedAlphas;

    bool isDivingFailed = false;
    bool isRecoverSpeedChanged = false;
    float recoverSpeed;
    Vector3 startPosition;
    Vector3 moveDirection;
    public enum State
    {
        Ready, Diving, DiveRecover ,Swimming, Finish
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
        float originalHeight = frameTransform.localPosition.y;
        while(eTime < diveDuration)
        {
            yield return null;

            eTime += Time.deltaTime;
            float yPos = Mathf.Lerp(originalHeight, diveDepth, TimeCurves.ExponentialMirrored(eTime / diveDuration));
            transform.position += moveDirection * diveSpeed * Time.deltaTime;
            frameTransform.localPosition = new Vector3(0, yPos, 0);
        }
        frameTransform.localPosition = new Vector3(0, diveDepth, 0);
        ChangeState(State.DiveRecover);
    }
    IEnumerator DiveRecover()
    {
        float swimmingSpeed = diveSpeed;
        float eTime = 0f;
        Vector3 originalPos = transform.position;
        recoverSpeed = -frameTransform.localPosition.y / recoverDuration;

        float currentSpeed = recoverSpeed;
        while (eTime < recoverDuration + bonusRecoverTotalDuration)
        {
            yield return null;
            eTime += Time.deltaTime;
            float t = eTime / recoverDuration;
            if (isRecoverSpeedChanged)
                recoverSpeed =- frameTransform.localPosition.y / (recoverDuration + bonusRecoverTotalDuration);
            swimmingSpeed = Mathf.Lerp(diveSpeed, defaultSwimmingSpeed, TimeCurves.Exponential(t));
            transform.position += moveDirection * swimmingSpeed * Time.deltaTime;
            frameTransform.localPosition += new Vector3(0, recoverSpeed, 0) * Time.deltaTime;
        }
        frameTransform.localPosition = new Vector3(0, 0, 0);
        ChangeState(State.Swimming);
    }
    IEnumerator Swimming()
    {
        currentSwimmingSpeed = defaultSwimmingSpeed;
        while (!isNewState)
        {
            yield return null;
            currentSwimmingSpeed = Mathf.Lerp(currentSwimmingSpeed, defaultSwimmingSpeed, Time.deltaTime);
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
        if (currentState == State.DiveRecover || currentState == State.Diving)
        {
            bonusRecoverTotalDuration += bonusRecoverDuration;
            isRecoverSpeedChanged = true;
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
    void SetWater(float normalizedValue)
    {
        waterImage.localPosition = new Vector3(0, Mathf.Lerp(waterImageMin,waterImageMax,normalizedValue));
    }
}
