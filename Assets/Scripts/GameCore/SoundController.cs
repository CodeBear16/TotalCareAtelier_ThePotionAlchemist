using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : Singleton<SoundController>
{
    public AudioSource source;
    [Tooltip("0: 스타트 장면 음악\n1: 인게임 평상시 음악\n2: 긴박한 상황 음악\n3: 굿엔딩 음악\n4: 배드엔딩 음악")]
    public AudioClip[] clips;

    public int musicState;
    public int MusicState
    {
        get
        {
            return musicState;
        }
        set
        {
            musicState = value;
            source.clip = clips[musicState];
            source.Play();
        }
    }

    void Start()
    {
        MusicState = 0;
    }
}
