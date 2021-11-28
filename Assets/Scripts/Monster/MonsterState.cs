using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterState : MonoBehaviour
{
    NavMeshAgent nav;
    public GameObject[] destination;

    private void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        // 목적지 갯수(플레이어 앞에 도달하는 장소)
        destination = new GameObject[6];
        destination = GameObject.FindGameObjectsWithTag("Destination");
    }

    public void WalkingToDestination()
    {
        //int selection = Random.Range(0, destination.Length);
        //nav.SetDestination(destination[selection].transform.position);
    }
}
