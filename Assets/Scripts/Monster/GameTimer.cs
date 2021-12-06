using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    TextMeshProUGUI currentTime;
    public int time = 60;
    MonsterState monsterState;

    private void Start()
    {
        monsterState = new MonsterState();
        currentTime = GetComponent<TextMeshProUGUI>();
        currentTime.text = time.ToString();
    }

    public void DecreaseTime()
    {
        time--;
    }

    public void ResetTime()
    {
        if (time == 0)
        {
            monsterState.monsterState = "DestinationToExit";
            monsterState.Walking();
            GameManager.instance.score -= 10;
        }

        time = 60;
    }
}
