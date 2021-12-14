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

    AsyncOperation startAsync;

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
    private const int winScore = 60; // 난이도 조절 가능

    private int heroDistance;
    public int HeroDistance
    {
        get { return heroDistance; }
        set
        {
            heroDistance = value;
            distanceWatch.text = heroDistance + "km";
            distanceSlider.value = giveDistance - heroDistance;

            if (heroDistance > 600) GoodEndEvent();
            if (heroDistance <= loseDistance) BadEndEvent();
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

        while (true)
        {
            yield return new WaitForSeconds(1);
            HeroDistance--;
        }
    }

    private IEnumerator TutorialTime()
    {
        yield return new WaitForSeconds(60);

        SpawnerCaller.instance.OnSpawner();
        DevilCaller.instance.OnDevil();

        StartCoroutine(GameTimer());
    }

    private void DestroyCore()
    {
        Destroy(PlayerBehaviour.instance.gameObject);
        Destroy(MonsterSpawner.instance.gameObject);
        Destroy(Pot.instance.gameObject);
    }
    #endregion



    #region Various Events
    public void GameStartEvent()
    {
        OVRScreenFade.instance.FadeOut();
        SoundController.instance.MusicLoader = 1;

        startAsync.allowSceneActivation = true;

        GameObject temp = GameObject.Find("Bottle_Blue");
        temp.GetComponent<OVRGrabbable>().GrabEnd(Vector3.zero, Vector3.zero);
        Destroy(temp);
        temp = GameObject.Find("Bottle_Red");
        temp.GetComponent<OVRGrabbable>().GrabEnd(Vector3.zero, Vector3.zero);
        Destroy(temp);

        OVRScreenFade.instance.FadeIn();

        StartCoroutine(TutorialTime());
    }

    public void GoodEndEvent()
    {
        SceneManager.LoadScene(2);
        SoundController.instance.StopMusic();
        DestroyCore();
    }

    public void BadEndEvent()
    {
        SceneManager.LoadScene(3);
        SoundController.instance.StopMusic();
        DestroyCore();
    }

    public void GameQuitEvent()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    #endregion
}
