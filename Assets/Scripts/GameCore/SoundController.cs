using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : Singleton<SoundController>
{
    public AudioSource[] sources;
    AudioSource tempSource;

    [Tooltip("0: 스타트 장면 음악\n1: 인게임 평상시 음악\n2: 긴박한 상황 음악\n3: 굿엔딩 음악\n4: 배드엔딩 음악")]
    public AudioClip[] clips;

    public int musicLoader = 0;
    public int MusicLoader
    {
        get
        {
            return musicLoader;
        }
        set
        {
            musicLoader = value;

            for (int i = 0; i < sources.Length; i++)
            {
                if (sources[i].isPlaying == false)
                {
                    tempSource = sources[i];
                    tempSource.clip = clips[musicLoader];
                    tempSource.Play();
                    CrossFade(tempSource, sources[Mathf.Abs(i - sources.Length + 1)]);
                    break;
                }
            }
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public IEnumerator CrossFade(AudioSource fadeIn, AudioSource fadeOut)
    {
        float fadeInVol = 0;
        float fadeOutVol = 1;

        while (fadeOut.volume > 0)
        {
            fadeInVol += Time.deltaTime * 100;
            if (fadeInVol >= 1)
                fadeInVol = 1;
            fadeIn.volume = fadeInVol;

            fadeOutVol -= Time.deltaTime * 100;
            if (fadeOutVol <= 0)
                fadeOutVol = 0;
            fadeOut.volume = fadeOutVol;

            yield return null;
        }
        fadeOut.Stop();
    }
}
