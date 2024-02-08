using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ef_Destory : MonoBehaviour
{
    public float destoryWaitTime = 0.5f;
    void Start()
    {
        Destroy(gameObject, destoryWaitTime);        
    }
    


}
