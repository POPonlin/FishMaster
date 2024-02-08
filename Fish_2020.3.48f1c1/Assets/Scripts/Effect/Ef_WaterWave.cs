using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ef_WaterWave : MonoBehaviour
{
    public Texture2D[] textures;
    private Material material;
    private int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
        InvokeRepeating("ChangeWave", 0f, 0.1f);
    }

    void ChangeWave()
    {
        material.mainTexture = textures[index];
        index = (index + 1) % textures.Length;
    }
}
