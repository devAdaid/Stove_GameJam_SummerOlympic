using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDataTest : MonoBehaviour
{
    public Text Template;
    public Transform TextRoot;

    private void Awake()
    {
        var mbtiDatas = GameData.I.MBTI.Datas;
        foreach (var data in mbtiDatas)
        {
            var text = Instantiate(Template, TextRoot);
            text.text = $"{data.MBTI} | {data.Endurance} | {data.Quickness} | {data.Strength} | {data.Flexibility}";
            text.gameObject.SetActive(true);
        }
    }
}
