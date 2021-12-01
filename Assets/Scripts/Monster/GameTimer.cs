using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameTimer : MonsterSpawner
{
    float timer = 60;
    public TextMeshProUGUI gameTimer;

    private void Start()
    {
        gameTimer.text = timer.ToString();
    }

    private void Update()
    {
        timer -= Time.deltaTime;
    }

    public float Timer
    {
        get
        {
            return timer;
        }

        set
        {
            timer = value;

            if(timer <= 0 || isSuccess == true)
                monster[spawnerCount].GetComponent<MonsterState>().state = "DestinationToSpawner";
        }
    }
}
