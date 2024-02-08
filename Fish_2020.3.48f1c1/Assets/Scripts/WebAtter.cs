using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebAtter : MonoBehaviour
{
    public float destoryTime;
    public int damage;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Fish")
        {
            collision.gameObject.GetComponent<FishAtter>().Dameged(damage);
        }
    }
}
