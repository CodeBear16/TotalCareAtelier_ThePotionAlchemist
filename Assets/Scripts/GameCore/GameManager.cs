using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : Singleton<GameManager>
{
    [Header("손목 UI")]
    public TextMeshProUGUI scoreWatch;
    public GameObject[] unhappyWatch;
    public TextMeshProUGUI distanceWatch;
    public Slider distanceSlider;

    #region Game Win or Lose Conditions
    private int score;
    public int Score
    {
        get { return score; }
        set
        {
            score = value;
            scoreWatch.text = score + "점";
            if (score >= winScore)
                GoodEndEvent();
        }
    }
    private const int winScore = 600;

    private int heroDistance;
    public int HeroDistance
    {
        get { return heroDistance; }
        set
        {
            heroDistance = value;
            distanceWatch.text = heroDistance + "km";
            distanceSlider.value = giveDistance - heroDistance;
            if (heroDistance <= loseDistance)
                BadEndEvent();
        }
    }
    private const int giveDistance = 600;
    private const int loseDistance = 0;

    private int monsterUnhappy = 0;
    public int MonsterUnhappy
    { 
        get { return monsterUnhappy; }
        set
        {
            monsterUnhappy = value;
            if (monsterUnhappy > 0)
                unhappyWatch[monsterUnhappy - 1].SetActive(true);
            if (monsterUnhappy >= unhappyWatch.Length)
                BadEndEvent();
        }
    }
    #endregion


    AsyncOperation startAsync;

    #region Methods
    private void Start()
    {
        HeroDistance = giveDistance;
        Score = 0;
        MonsterUnhappy = 0;
        SoundController.instance.MusicLoader = 0;

        startAsync = SceneManager.LoadSceneAsync(1);
        startAsync.allowSceneActivation = false;
    }

    private IEnumerator GameTimer()
    {
        yield return new WaitForSeconds(3);

        Debug.Log("타이머 시작");
        while (true)
        {
            yield return new WaitForSeconds(1);
            HeroDistance--;
        }
    }
    #endregion


    #region Various Events
    public void GameStartEvent()
    {
        Debug.Log("게임 시작");
        OVRScreenFade.instance.FadeOut();
        SoundController.instance.MusicLoader = 1;
        OVRScreenFade.instance.FadeIn();
        startAsync.allowSceneActivation = true;

        GameObject temp = GameObject.Find("Bottle_Blue");
        temp.GetComponent<OVRGrabbable>().GrabEnd(Vector3.zero, Vector3.zero);
        Destroy(temp);
        temp = GameObject.Find("Bottle_Red");
        temp.GetComponent<OVRGrabbable>().GrabEnd(Vector3.zero, Vector3.zero);
        Destroy(temp);

        StartCoroutine(GameTimer());
    }

    public void GoodEndEvent()
    {
        Debug.Log("승리!");
        SceneManager.LoadScene(2);
        //SoundController.instance.MusicLoader = 3;
        Destroy(PlayerBehaviour.instance.gameObject);
    }

    public void BadEndEvent()
    {
        Debug.Log("패배...");
        SceneManager.LoadScene(3);
        //SoundController.instance.MusicLoader = 4;
        Destroy(PlayerBehaviour.instance.gameObject);
        Destroy(MonsterSpawner.instance.gameObject);
        Destroy(Pot.instance.gameObject);
    }

    public void GameQuitEvent()
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
