using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource aS => GetComponent<AudioSource>();

    private void Awake()
    {
        AudioManager[] objs = FindObjectsOfType<AudioManager>();

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public void Play()
    {
        aS.Play();
    }

    public void Pause()
    {
        aS.Pause();
    }
}
