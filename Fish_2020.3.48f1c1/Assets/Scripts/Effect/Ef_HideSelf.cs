using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ef_HideSelf : MonoBehaviour
{
    public IEnumerator HideSelf(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
