using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterTest : MonoBehaviour
{
    NavMeshAgent nav;
    // 이동할 목적지
    public Transform targetPos;
    MonsterTest2 monsterTest2;

    private void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        monsterTest2 = new MonsterTest2();
    }

    private void Update()
    {
        nav.SetDestination(targetPos.position);
        monsterTest2.Test();
    }
}
