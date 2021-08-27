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
    [SerializeField] float normalSwimmingSpeed;

    public enum State
    {
        Ready, DiveReady, Diving, DiveRecover ,Swimming, Finish
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

    IEnumerator Ready()
    {
        ChangeState(State.DiveReady);
        yield return null;
    }
    IEnumerator DiveReady()
    {
        while(!isNewState)
        {
            yield return null;
            if(DiveButtonPressed())
            {
                ChangeState(State.Diving);
            }
        }
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
            transform.position += new Vector3(diveSpeed, diveDepth / diveDuration, 0) * Time.deltaTime;
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
        while (eTime < recoverDuration)
        {
            yield return null;
            eTime += Time.deltaTime;
            float t = eTime / recoverDuration;
            swimmingSpeed = Mathf.Lerp(diveSpeed, normalSwimmingSpeed, TimeCurves.Exponential(t));
            transform.position += new Vector3(swimmingSpeed, recoverHeight / recoverDuration, 0) * Time.deltaTime;
        }
        transform.position = new Vector3(transform.position.x, originalPos.y + recoverHeight, 0);
        ChangeState(State.Swimming);
    }
    IEnumerator Swimming()
    {
        while(!isNewState)
        {
            yield return null;
            transform.position += new Vector3(normalSwimmingSpeed, 0, 0) * Time.deltaTime;
        }
    }
    IEnumerator Finish()
    {
        yield return null;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("!");
        if(collision.tag.Equals("FinishLine"))
        {
            Debug.Log("!2");
            if (currentState == State.Swimming)
            {
                Debug.Log("3");
                ChangeState(State.Finish);
            }
        }
    }
    bool DiveButtonPressed()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }

    void ProcessInputs()
    {
    }
    private void Update()
    {
        ProcessInputs();
    }
}
