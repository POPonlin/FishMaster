using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ef_AutoRoat : MonoBehaviour
{
    public float speed = 1f;

    void Update()
    {
        transform.Rotate(Vector3.forward, speed * Time.deltaTime);
    }
}
