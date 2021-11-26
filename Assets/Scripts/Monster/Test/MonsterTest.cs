using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterTest : MonoBehaviour
{
    NavMeshAgent nav;
    // 이동할 목적지
    public Transform targetPos;

    private void Start()
    {
        nav = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        nav.SetDestination(targetPos.position);
    }
}
