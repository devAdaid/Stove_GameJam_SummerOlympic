using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimEffectManager : MonoBehaviour
{
    public GameObject diveParticle;
    public GameObject swimParticle;
    public void SpawnEffect(GameObject prefab, Vector3 position)
    {
        Instantiate(prefab, position, Quaternion.identity);
    }
}
