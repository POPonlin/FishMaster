using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAtter : MonoBehaviour
{
    public int maxNum;
    public int maxSpeed;
    public int blood = 10;
    public float exp;
    public int gold;
    public GameObject dieFish;
    public GameObject coins;

    public float invincibilityDuration = 0.3f; // 受伤后的无敌持续时间
    private bool isInvincible = false; // 是否处于无敌状态
    private float invincibilityTimer = 0.0f; // 无敌状态计时器
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Border")
        {
            Destroy(gameObject);
        }
    }

    public void Dameged(int damage)
    {
        if (!isInvincible)
        {
            blood -= damage;
            if (blood <= 0)
            {
                GameControler.Instance.Exp += exp;
                GameControler.Instance.Scores += exp;
                GameControler.Instance.OwnGold += gold;                

                GameObject die = Instantiate(dieFish);
                die.transform.SetParent(transform.parent, false);
                die.transform.localPosition = transform.localPosition;
                die.transform.localRotation = transform.localRotation;

                GameObject c = Instantiate(coins);
                c.transform.SetParent(transform.parent, false);
                c.transform.localPosition = transform.localPosition;
                c.transform.localRotation = transform.localRotation;
                c.AddComponent<Ef_MoveTo>().targetPos = GameObject.Find("GoldCollect").transform.position;
                die.AddComponent<Ef_Destory>();  
            
                if (gameObject.TryGetComponent<Ef_Particle>(out var ef))
                {
                    ef.StartParticle();
                    AudioManger.Instance.PlayMusic(AudioManger.Instance.gold);
                }

                Destroy(gameObject);            
            }
            isInvincible = true; // 进入无敌状态
        }

    }

    void Update()
    {
        if (isInvincible)
        {
            invincibilityTimer += Time.deltaTime;
            if (invincibilityTimer >= invincibilityDuration)
            {
                isInvincible = false;
                invincibilityTimer = 0.0f;
            }
        }
    }
}
