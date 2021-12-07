using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : Singleton<GameManager>
{
    // 스코어 조건
    public TextMeshProUGUI scoreWatch;
    public int score;

    private void Start()
    {
        score = 0;
        isGameOver = false;
        monsterUnhappy = 0;
    }

    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
            //잠깐 수정함 쏘리
            //scoreWatch.text = score + "점";
            if (score >= winScore)
                GoodEndEvent();
        }
    }

    public const int winScore = 600;

    public TextMeshProUGUI heroTimeWatch;
    public int heroTime;
    public int HeroTime
    {
        get
        {
            return heroTime;
        }
        set
        {
            heroTime = value;
            //잠깐 수정함 쏘리
            //heroTimeWatch.text = ToWatch(heroTime);
            if (heroTime >= loseTime)
                BadEndEvent();
        }
    }
    public const int loseTime = 600;

    public override void Awake()
    {
        base.Awake();
        Debug.Log(instance);
        DontDestroyOnLoad(gameObject);
        Score = 0;
        HeroTime = 0;
    }

    public string ToWatch(int time)
    {
        return (time / 60) + "분 " + (time % 60) + "초";
    }

    public IEnumerator GameTimer()
    {
        while (true)
        {
            yield return null;
        }
    }

    // 게임 오버 조건
    public bool isGameOver;
    public int monsterUnhappy;

    public int MonsterUnhappy
    { 
        get
        {
            return monsterUnhappy;
        }

        set
        {
            monsterUnhappy = value;
            if (monsterUnhappy == 4) BadEndEvent();
        }
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
