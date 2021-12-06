using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : Singleton<GameManager>
{
    Dictionary<int, string> foodList = new Dictionary<int, string>();
    Dictionary<int, string> foodKindList = new Dictionary<int, string>();
    public void MuMukG()
    {
        foodList.Add(0, "한식");
        foodList.Add(1, "중식");
        foodList.Add(2, "양식");
        foodList.Add(3, "일식");

        foodKindList.Add(0, "면");
        foodKindList.Add(1, "밥");
        foodKindList.Add(2, "빵");
        foodKindList.Add(3, "고기");

        int alcohol = Random.Range(0, 3);


        int kind = 0;
        kind = Random.Range(0, 3);
        int foodKind = Random.Range(0, 3);
        Debug.Log(" 음식 뭐먹지? " + foodList[kind]);
        Debug.Log(" 음식 종류 : " + foodKindList[foodKind]);
        if (alcohol == 1)
            Debug.Log("마셔마셔~");
        else
            Debug.Log("술은 마시지 말자");
    }

    public TextMeshProUGUI scoreWatch;
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



    private new void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        Score = 0;
        HeroTime = 0;
        MuMukG();
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
