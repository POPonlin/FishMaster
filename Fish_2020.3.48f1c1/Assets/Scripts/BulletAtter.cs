using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAtter : MonoBehaviour
{
    public float speed;
    public int damage;
    public GameObject web;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Fish")
        {
            GameObject tmp = Instantiate(web);
            tmp.transform.SetParent(transform.parent, false);
            tmp.transform.position = transform.position;
            tmp.transform.rotation = transform.rotation;
            tmp.AddComponent<Ef_Destory>().destoryWaitTime = tmp.GetComponent<WebAtter>().destoryTime;
            tmp.GetComponent<WebAtter>().damage = damage;
            AudioManger.Instance.PlayMusic(AudioManger.Instance.web);
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Border")
        {
            Destroy(gameObject);
        }
    }
}
