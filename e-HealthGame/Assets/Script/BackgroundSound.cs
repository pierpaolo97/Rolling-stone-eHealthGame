using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSound : MonoBehaviour
{
    private static BackgroundSound instance = null;
    public AudioSource audioback;
    private static BackgroundSound Instance
    {
        get{return instance; }
    }


    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        audioback.volume = PlayerPrefs.GetFloat("musicvolume", 0.25f);
        DontDestroyOnLoad(this.gameObject);
        Debug.Log(PlayerPrefs.GetFloat("musicvolume"));
    }
}
