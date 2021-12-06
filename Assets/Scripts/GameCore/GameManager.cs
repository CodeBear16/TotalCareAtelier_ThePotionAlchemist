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
    public const int loseDistance = 1000;

    private new void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        Score = 0;
        HeroImminent = 0;
    }



    #region Various Events
    public void GameStartEvent()
    {
        Debug.Log("게임 시작");
        SceneManager.LoadScene(1);
        SoundController.instance.MusicLoader = 1;
    }

    public void DemonIncomeEvent()
    {
        Debug.Log("방해꾼 도착");
        SoundController.instance.MusicLoader = 2;
    }

    public void GoodEndEvent()
    {
        Debug.Log("승리!");
        SceneManager.LoadScene(2);
        SoundController.instance.MusicLoader = 3;
    }

    public void BadEndEvent()
    {
        Debug.Log("패배...");
        SceneManager.LoadScene(3);
        SoundController.instance.MusicLoader = 4;
    }

    public void GameEndEvent()
    {
        Debug.Log("게임 종료");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
#endregion
}
