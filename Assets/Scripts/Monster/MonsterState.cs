using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterState : MonoBehaviour
{
    public MonsterSpawner monsterSpawner;
    Transform spawnerPosition;
    
    GameObject player;
    Transform playerPosition;
    public bool isSuccess; // ------------------------------ player와 연동해야 함, 삭제 필

    // monster 이동
    public NavMeshAgent nav;
    public List<GameObject> destinations;
    public Animator animator;

    // monster state
    string monsterState = "MonsterState";

    // switch 값
    public string state;

    // isHeal property
    public bool IsSuccess
    {
        get
        {
            return isSuccess;
        }

        set
        {
            isSuccess = value;

            if (isSuccess == true)
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
        isSuccess = false;
    }

    public void Setting()
    {
        destinations = MonsterSpawner.destinationsSpotList;
    }

    public void Walking(int num)
    {
        switch (state)
        {
            // [---------------- spawner에서 destination으로 이동 ----------------]
            case "SpawnerToDestination":

                while (num > 0)
                {
                    if (num == 0) break;

                    animator.SetBool("Walking", true);
                    playerPosition = player.transform;
                    int selectDestination = Random.Range(0, destinations.Count);
                    GameObject destination = destinations[selectDestination];

                    if (destination.activeSelf == false)
                    {
                        nav.SetDestination(destination.transform.position);
                        destination.SetActive(true);
                        num--;
                        break;
                    }

                    // destination이 활성화 상태일 때, 다시 비활성화 상태인 destination을 찾는 것
                    else
                    {
                        continue;
                    }

                }
                break;

            // [---------------- destination에서 spawner로 이동 ----------------]
            case "DestinationToSpawner":

                // 성공
                if(isSuccess == true)
                {
                    animator.SetBool("Drinking", true);
                    animator.SetBool("Walking", true);
                    spawnerPosition = monsterSpawner.gameObject.transform;
                    nav.SetDestination(spawnerPosition.position);
                }

                // 실패
                else
                {
                    animator.SetBool("SadWalk", true);
                    spawnerPosition = monsterSpawner.gameObject.transform;
                    nav.SetDestination(spawnerPosition.position);
                }

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
            if (other.tag == "Spawner")
            {
                nav.speed = 0;
                gameObject.SetActive(false);
            }
        }
    }
}
