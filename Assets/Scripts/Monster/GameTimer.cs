using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameTimer : MonsterSpawner
{
    public List<GameObject> timer;
    float time = 60;
    TextMeshProUGUI gameTimer;

    private void Start()
    {
        timer = new List<GameObject>(GameObject.FindGameObjectsWithTag("Timer"));
        
        for(int i = 0; i < timer.Count; i ++)
        {
            destinations[i] = timer[i];
        }
        
        gameTimer = GameObject.FindGameObjectWithTag("Timer").transform.GetComponent<TextMeshProUGUI>();
        gameTimer.text = timer.ToString();
    }

    public void DecreaseTime()
    {
        while(time <= 0)
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

            if(time <= 0 || isSuccess == true)
            {
                monster[spawnerCount].GetComponent<MonsterState>().state = "DestinationToSpawner";
                time = 60;
            } 
        }
    }
}
