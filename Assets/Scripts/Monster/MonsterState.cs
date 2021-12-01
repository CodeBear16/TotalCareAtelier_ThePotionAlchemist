using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterState : MonoBehaviour
{
    public MonsterSpawner monsterSpawner;
    
    GameObject player;
    Transform playerPosition;

    // monster 이동
    public NavMeshAgent nav;
    public List<GameObject> destinations;
    public Animator animator;

    // monster state
    string monsterState = "MonsterState";

    // switch 값
    public string state;

    public bool isHeal = false;

    // isHeal property
    public bool IsHeal
    {
        get
        {
            return isHeal;
        }

        set
        {
            isHeal = value;

            if (isHeal == true)
                GameManager.instance.score += 100;

            else
                GameManager.instance.score -= 100;
        }
    }

    private void OnEnable()
    {
        nav = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Target");
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        
    }

    public void Setting()
    {
        destinations = MonsterSpawner.destinationsSpotList;
    }

    public void Walking()
    {
        switch (state)
        {
            // spawner에서 destination으로 이동
            case "SpawnerToDestination":

                animator.SetBool("Walking", true);
                playerPosition = player.transform;
                int selectDestination = Random.Range(0, destinations.Count);
                GameObject destination = destinations[selectDestination];

                if (destination.activeSelf == false)
                {
                    nav.SetDestination(destination.transform.position);
                    destination.SetActive(true);
                }

                // destination이 활성화 상태일 때, 다시 비활성화 상태인 destination을 찾는 것
                else
                {
                    
                }

                break;

            // destination에서 spawner로 이동
            case "DestinationToSpawner":

                // if : drinking을 했을 때는 그냥 이동
                // else : 화났을 때, sad wallking

                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // monster가 destination에 도착하면 멈춤
        if (other.tag == "Destination")
        {
            animator.SetBool("Walking", false);
            transform.LookAt(playerPosition.position);
            nav.speed = 0;
            int aniSelection = Random.Range(1, 6);
            Debug.Log(gameObject.name + " : " + aniSelection);
            animator.SetInteger(monsterState, aniSelection);
        }

        // monster가 spawner에 도착하면 비활성화
        if (state == "DestinationToSpawner")
        {
            if (other.tag == "Spawner") gameObject.SetActive(false);
        }
    }
}
