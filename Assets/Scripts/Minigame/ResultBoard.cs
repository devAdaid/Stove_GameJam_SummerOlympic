using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultBoard : MonoBehaviour
{
    [SerializeField] Sprite[] flags;
    [SerializeField] Image[] flagImages;
    [SerializeField] Text[] nameTexts;
    [SerializeField] Text[] recordTexts;

    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetValues(List<AthleteFSM> athletes)
    {
        for(int i=0;i<athletes.Count;i++)
        {
            flagImages[i].sprite = flags[athletes[i].flagType];
            nameTexts[i].text = athletes[i].name;
            recordTexts[i].text = ((int)athletes[i].finishedTime / 60).ToString().PadLeft(2, '0') + ":"
            + string.Format("{0:00.00}", (athletes[i].finishedTime % 60));
        }
    }
    public void Close()
    {
        animator.SetTrigger("Close");
    }
}
