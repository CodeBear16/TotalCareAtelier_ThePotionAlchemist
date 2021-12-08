using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class VideoPlayer : MonoBehaviour
{
    VideoPlayer video;
    public int time;

    private void Start()
    {
        video = GetComponent<VideoPlayer>();
        Invoke("GameEnd", time);
    }

    void GameEnd()
    {
        GameManager.instance.GameQuitEvent();
    }
}
