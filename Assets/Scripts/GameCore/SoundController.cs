using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : Singleton<SoundController>
{
    [Header("플레이어의 오디오 소스 2개")]
    public AudioSource[] sources;
    private AudioSource tempSource;

    [Header("배경음악(상세클립은 툴팁 안내)")]
    [Tooltip("0: 스타트 장면 음악\n1: 인게임 평상시 음악\n2: 긴박한 상황 음악\n3: 굿엔딩 음악\n4: 배드엔딩 음악")]
    public AudioClip[] clips;

    [Header("현재 재생 중인 배경음악 클립")]
    private int musicLoader = 0;
    public int MusicLoader
    {
        get { return musicLoader; }
        set
        {
            musicLoader = value;

            for (int i = 0; i < sources.Length; i++)
            {
                if (sources[i].isPlaying == false)
                {
                    tempSource = sources[i];
                    tempSource.clip = clips[musicLoader];
                    StartCoroutine(CrossFade(tempSource, sources[Mathf.Abs(i - sources.Length + 1)]));
                    break;
                }
            }
        }
    }

    private IEnumerator CrossFade(AudioSource fadeIn, AudioSource fadeOut)
    {
        float fadeInVol = 0;
        float fadeOutVol = 1;

        fadeIn.Play();
        while (fadeOut.volume > 0)
        {
            fadeInVol += Time.deltaTime/2;
            if (fadeInVol >= 1)
                fadeInVol = 1;
            fadeIn.volume = fadeInVol;

            fadeOutVol -= Time.deltaTime/2;
            if (fadeOutVol <= 0)
                fadeOutVol = 0;
            fadeOut.volume = fadeOutVol;

            yield return null;
        }
        fadeOut.Stop();
    }

    public void StopMusic()
    {
        sources[0].Stop();
        sources[1].Stop();
    }
}
