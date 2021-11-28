using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterState : MonoBehaviour
{
    public MonsterSpawner monsterSpawner;
    NavMeshAgent nav;
    // 목적지 배열(플레이어 앞에 도달하는 장소)
    public GameObject[] destination;

    private void OnEnable()
    {
        StartCoroutine(Disappear());
    }

    private void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        destination = new GameObject[6];
        destination = Resources.LoadAll<GameObject>("MonsterDestination/");
    }

    // 플레이어 앞으로 이동
    public void WalkingToDestination()
    {
        // null 오류 확인하기
        //int selection = Random.Range(0, destination.Length);
        //nav.SetDestination(destination[selection].transform.position);
    }

    // 몬스터가 spawner로 돌아왔을 때 비활성화
    IEnumerator Disappear()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);

            // 몬스터와 spawner의 거리 차이가 0.05보다 작으면 몬스터 비활성화
            if (Vector3.Distance(monsterSpawner.transform.position, transform.position) < 0.05f)
            {
                gameObject.SetActive(false);
                Debug.Log(gameObject.name + "이 비활성화 되었습니다");
                monsterSpawner.spawnerCount--;
            }
        }
    }
}
