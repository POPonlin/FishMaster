using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ef_Particle : MonoBehaviour
{
    public ParticleSystem[] particle;

    public void StartParticle()
    {
        foreach (var p in particle) 
        { 
            ParticleSystem t = Instantiate(p);
            t.gameObject.AddComponent<Ef_Destory>().destoryWaitTime = 1f;
        }
    }
}
