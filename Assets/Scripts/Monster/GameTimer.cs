using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    GameObject gameTimer;
    public List<GameObject> timer;
    TextMeshProUGUI currentTime;
    float time = 60;

    private void Start()
    {
        gameTimer = this.gameObject;
        timer = new List<GameObject>();

        for (int i = 0; i < gameTimer.transform.childCount; i++)
            timer.Add(gameTimer.transform.GetChild(i).gameObject);
        
        currentTime.text = timer.ToString();
    }

    public void DecreaseTime()
    {
        while(time >= 0 && time <= 60)
            time -= Time.deltaTime;
    }

    public float Timer
    {
        get
        {
            return time;
        }

        set
        {
            time = value;

            //if(time <= 0 || isSuccess == true)
            //{
            //    monsterPool[spawnCount].GetComponent<MonsterState>().monsterState = "DestinationToExit";
            //    time = 60;
            //} 
        }
    }
}
