using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public int score;
    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
            if (score >= winScore)
                GoodEndEvent();
        }
    }
    public const int winScore = 1000;

    public int heroImminent;
    public int HeroImminent
    {
        get
        {
            return heroImminent;
        }
        set
        {
            heroImminent = value;
            if (heroImminent >= loseDistance)
                BadEndEvent();
        }
    }
    public const int loseDistance = 100;

    void Start()
    {
        Score = 0;
        HeroImminent = 0;
    }

    #region Various Events
    public void GoodEndEvent()
    {
        Debug.Log("½Â¸®!");
        SceneManager.LoadScene(2);
        SoundController.instance.MusicState = 3;
    }

    public void BadEndEvent()
    {
        Debug.Log("ÆÐ¹è...");
        SceneManager.LoadScene(3);
        SoundController.instance.MusicState = 4;
    }
    #endregion
}
