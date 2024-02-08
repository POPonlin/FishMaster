using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMaker : MonoBehaviour
{
    public Transform[] genPos;
    public GameObject[] fishPrefabs;
    public float genFishWaitTime = 0.3f;
    public float waveFishWaitTime = 0.5f;

    public Transform fishPanel;
    void Start()
    {
        InvokeRepeating("GenFish", 0f, waveFishWaitTime);
    }

    void GenFish()
    {
        int posIndex = Random.Range(0, genPos.Length);
        int fishIndex = Random.Range(0, fishPrefabs.Length);
        int maxNum = fishPrefabs[fishIndex].GetComponent<FishAtter>().maxNum;
        int maxSpeed = fishPrefabs[fishIndex].GetComponent<FishAtter>().maxSpeed;

        int num = Random.Range(maxNum / 2 + 1, maxNum + 1);
        float speed = Random.Range(maxSpeed / 2, maxSpeed + 1);

        int flag = Random.Range(0, 2);
        if (flag == 0) //走直线
        {
            int angoff = Random.Range(-22, 22);
            StartCoroutine(GenStraightFish(num, posIndex, fishIndex, speed, angoff));
        }
        else //走曲线
        {
            int angSpeed;
            if (Random.Range(0, 2) == 0)
            {
                angSpeed = Random.Range(15, 22);
            }
            else
            {
                angSpeed = Random.Range(-15, -22);
            }
            StartCoroutine(GenRoateFish(num, posIndex, fishIndex, speed, angSpeed));
        }
    }

    IEnumerator GenStraightFish(int num, int posIndex, int fishIndex, float speed, int angoff)
    {
        for(int i = 0; i < num; i++)
        {
            GameObject fish = Instantiate(fishPrefabs[fishIndex]);
            fish.transform.SetParent(fishPanel, false);
            fish.transform.localPosition = genPos[posIndex].localPosition;
            fish.transform.localRotation = genPos[posIndex].localRotation;
            fish.transform.Rotate(0, 0, angoff);

            fish.GetComponent<SpriteRenderer>().sortingOrder += i; 
            fish.AddComponent<Ef_AutoMove>().speed = speed;

            yield return new WaitForSeconds(genFishWaitTime);
        }
    }

    IEnumerator GenRoateFish(int num, int posIndex, int fishIndex, float speed, int angoff)
    {
        for (int i = 0; i < num; i++)
        {
            GameObject fish = Instantiate(fishPrefabs[fishIndex]);
            fish.transform.SetParent(fishPanel, false);
            fish.transform.localPosition = genPos[posIndex].localPosition;
            fish.transform.localRotation = genPos[posIndex].localRotation;
            //fish.transform.Rotate(0, 0, angoff);

            fish.GetComponent<SpriteRenderer>().sortingOrder += i;
            fish.AddComponent<Ef_AutoMove>().speed = speed;
            fish.AddComponent<Ef_AutoRoat>().speed = angoff;
            yield return new WaitForSeconds(genFishWaitTime);
        }
    }
}
