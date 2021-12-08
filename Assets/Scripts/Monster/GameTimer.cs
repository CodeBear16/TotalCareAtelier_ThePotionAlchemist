using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    TextMeshProUGUI currentTime;
    public int time;
    MonsterState monsterState;

    private void Start()
    {
        time = 20;
        monsterState = GetComponentInParent<MonsterState>();
        currentTime = GetComponent<TextMeshProUGUI>();
        currentTime.text = time.ToString();
    }

    public void DecreaseTime()
    {
        StartCoroutine(DecreaseingTime());
    }

    public IEnumerator DecreaseingTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            time--;
            currentTime.text = time.ToString();
            if (time <= 0)
            {
                ResetTime();
                break;
            }
        }
    }

    public void ResetTime()
    {
        if (time <= 0)
        {
            GameManager.instance.Score -= 10;
            GameManager.instance.HeroDistance -= 10;
            GameManager.instance.MonsterUnhappy++;
            monsterState.animator.SetBool("SadWalk", true);
        }

        monsterState.monsterState = "DestinationToExit";
        monsterState.Walking();
    }
}
