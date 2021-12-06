using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : Singleton<PlayerBehaviour>, IAttackFunc, IGetHitFunc
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Attack()
    {

    }

    public void GetHit()
    {

    }
}
