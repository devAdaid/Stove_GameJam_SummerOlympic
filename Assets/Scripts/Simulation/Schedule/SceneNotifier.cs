using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneNotifier : MonoBehaviour
{
    void Awake()
    {
        Simulation.I.OnScheduleScene();
    }
}
