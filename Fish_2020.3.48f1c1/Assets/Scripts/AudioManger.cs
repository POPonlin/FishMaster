using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManger : MonoBehaviour
{
    public AudioClip levelUp;
    public AudioClip fire;
    public AudioClip changeGun;
    public AudioClip web;
    public AudioClip ag;
    public AudioClip gold;
    public AudioClip fireNull;
    public AudioClip[] bgAudios;

    private AudioSource audioSource;

    private static AudioManger instance;
    public static AudioManger Instance
    {
        get
        {
            return instance;
        }
    }

    public bool isOpen = true;
    public bool isOpen1 = true;
    public void Awake()
    {
        instance = this;
        audioSource= GetComponent<AudioSource>();
        audioSource.clip = bgAudios[0];
    }

    public void SwitchGBMusic(bool isOn)
    {
        isOpen = isOn;

        DoBGMusic();
    }

    public void DoBGMusic()
    {
        if (isOpen)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }
    }


    public void SwitchAudio(bool isOn)
    {
        isOpen1 = isOn;
    }

    public void PlayMusic(AudioClip clip)
    {
        if (isOpen1)
        {
            AudioSource.PlayClipAtPoint(clip, new Vector3(0f, 0f, -10f));
        }
    }

    public void ChangeBGMusic(int index)
    {
        audioSource.clip = bgAudios[index];
    }
}
