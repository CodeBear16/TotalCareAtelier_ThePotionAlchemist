using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterState : MonoBehaviour
{
    public MonsterSpawner monsterSpawner;

    // monster 이동
    public NavMeshAgent nav;
    public List<GameObject> destinations;
    public Animator animator;

    private void OnEnable()
    {
        StartCoroutine(Disappear());
        nav = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Setting()
    {
        destinations = MonsterSpawner.destinationsSpotList;
        Debug.Log("스타트 테스트 : " + destinations.Count);
    }

    // 플레이어 앞으로 이동
    public void WalkingToDestination()
    {
        Debug.Log("이동 테스트 : " + destinations.Count);
        animator.SetBool("Walking", true);
        int selectDestination = Random.Range(0, destinations.Count);
        GameObject destination = destinations[selectDestination];

        Debug.Log("이동 포인트  : " + destination.name + selectDestination);

        if (destination.activeSelf == false)
        {
            nav.SetDestination(destination.transform.position);
            destination.SetActive(true);
        }

        else
        {
            //WalkingToDestination();
        }
    }

    // 몬스터가 spawner로 돌아왔을 때 비활성화
    IEnumerator Disappear()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);

            // 몬스터와 spawner의 거리 차이가 0.001보다 작으면 몬스터 비활성화
            if (Vector3.Distance(monsterSpawner.transform.position, transform.position) < 0.001f)
            {
                gameObject.SetActive(false);
                Debug.Log(gameObject.name + "이 비활성화 되었습니다");
                monsterSpawner.spawnerCount--;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Destination") animator.SetBool("Walking", false);
    }
}
