using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Manager : MonoBehaviour
{
    private static Audio_Manager instance;

    //public void Awake()
    //{
    //    if (null == instance)
    //    {
    //        instance = this;
    //        DontDestroyOnLoad(this.gameObject);
    //    }
    //    else
    //    {
    //        Destroy(this.gameObject);
    //    }
    //}

    [Header("---------- 오디오 소스 ----------")]
    [SerializeField]  AudioSource MusicSource;
    [SerializeField]  AudioSource SFXSource;


    [Header("---------- 오디오 클립 ----------")]
    public AudioClip Loby_background;
    public AudioClip Main_background;

    public void Loby_Music()
    {
        MusicSource.clip = Loby_background;
        MusicSource.Play();
    }

    public void Main_Music()
    {
        MusicSource.clip = Main_background;
        MusicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

}


