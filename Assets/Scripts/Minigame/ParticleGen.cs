using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleGen : MonoBehaviour
{
    public AthleteFSM athlete;

    public void SpawnSwimParticle()
    {
        athlete.SwimParticle();
    }
}
