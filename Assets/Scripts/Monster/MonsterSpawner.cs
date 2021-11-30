using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MonsterSpawner : MonoBehaviour
{
    public static List<GameObject> destinationsSpotList = new List<GameObject>();
    // 복제할 몬스터들
    public GameObject[] monsters;
    // 몬스터 스포너
    public List<GameObject> monstersSpawner = new List<GameObject>();
    // 나타날 몬스터의 최대수
    const int spawnerMaxSize = 2;
    // 몬스터의 현재 수
    public int spawnerCount = 0;

    // 임시 객체
    GameObject tempObject;

    private void Start()
    {
        monsters = Resources.LoadAll<GameObject>("Monster/");

        // 몬스터들의 목적지 리스트 설정
        destinationsSpotList = new List<GameObject>(GameObject.FindGameObjectsWithTag("Destination"));
        for (int i = 0; i < destinationsSpotList.Count; i++)
            destinationsSpotList[i].SetActive(false);

        // 스포너에 몬스터 6마리 넣어놓기(오브젝트 풀링)
        for (int i = 0; i < monsters.Length; i++)
        {
            tempObject = Instantiate(monsters[i], transform.position, transform.rotation);
            tempObject.GetComponent<MonsterState>().monsterSpawner = this;
            tempObject.transform.parent = transform;
            tempObject.SetActive(false);
            monstersSpawner.Add(tempObject);
        }
        
        StartCoroutine(SpawnMonster());
    }

    IEnumerator SpawnMonster()
    {
        while (true)
        {
            Spawn();
            // 20~40초에 한 번씩 몬스터 활성화
            yield return new WaitForSeconds(UnityEngine.Random.Range(3,5));
        }
    }
    int temp = 0;
    void Spawn()
    {
        // 몬스터 랜덤 생성
        //int selection = UnityEngine.Random.Range(0, monsters.Length);
        GameObject monster = monstersSpawner[temp];
        monster.transform.position = transform.position;
        temp++;

        // 이미 활성화된 몬스터
        if (monster.activeSelf == true) return;

        // 몬스터 활성화
        if (spawnerCount < spawnerMaxSize)
        {
            monster.SetActive(true);
            Debug.Log(monster.name + "생성되었습니다");
            GameObject.FindWithTag("Door").GetComponent<DoorOpen>().Open();
            monster.GetComponent<MonsterState>().Setting();
            monster.GetComponent<MonsterState>().state =  "SpawnerToDestination";
            monster.GetComponent<MonsterState>().Walking();
            spawnerCount++;
        }
    }
}
