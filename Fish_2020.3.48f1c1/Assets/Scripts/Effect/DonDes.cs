using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonDes : MonoBehaviour
{
    private static DonDes instance;
    public static DonDes Instance { get { return instance; } }

    public bool openChallenge = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
