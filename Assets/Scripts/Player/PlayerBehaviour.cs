using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : Singleton<PlayerBehaviour>, IAttackFunc, IGetHitFunc
{
    [Header("게임 시작 시간")]
    public int time = 0;

    public void Attack()
    {
        StartCoroutine(IncreasingTime());
    }

    IEnumerator IncreasingTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            time++;
        }
    }

    public void GetHit()
    {

    }
}
