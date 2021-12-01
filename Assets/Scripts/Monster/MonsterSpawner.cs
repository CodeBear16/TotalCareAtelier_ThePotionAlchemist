using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MonsterSpawner : MonoBehaviour
{
    // 도착지 리스트
    public static List<GameObject> destinationsSpotList = new List<GameObject>();
    // 복제할 몬스터들
    public GameObject[] monsters;
    // 몬스터 스포너에서 나온 몬스터
    public List<GameObject> monster = new List<GameObject>();
    // 나타날 몬스터의 최대수
    const int spawnerMaxSize = 100;
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

        // 스포너에 있는 몬스터들 (100마리 미리 넣어놓는 것)
        for (int i = 0; i < spawnerMaxSize; i++)
        {
            int selection = UnityEngine.Random.Range(0, monsters.Length);
            tempObject = Instantiate(monsters[selection], transform.position, transform.rotation);
            tempObject.GetComponent<MonsterState>().monsterSpawner = this;
            tempObject.transform.parent = transform; // 하이어라키창 정리
            tempObject.SetActive(false);
            monster.Add(tempObject);
        }
        StartCoroutine(SpawnMonster());
    }

    IEnumerator SpawnMonster()
    {
        while (true)
        {
            Spawn();
            // 20~30초에 한 번씩 몬스터 활성화
            yield return new WaitForSeconds(UnityEngine.Random.Range(20,30));
        }
    }
   
    void Spawn()
    {
        spawnerCount++;
        // spawnerCount 가 100보다 커지면 다시 처음으로 돌아가게 한다. 
        if (spawnerCount > spawnerMaxSize) spawnerCount = 0;

        // 이미 활성화된 몬스터
        if (monster[spawnerCount].activeSelf == true) return;

        // 몬스터 랜덤 생성
        monster[spawnerCount].transform.position = transform.position;
        monster[spawnerCount].SetActive(true);
        Debug.Log(monster[spawnerCount].name + "생성되었습니다");
        monster[spawnerCount].GetComponent<MonsterState>().Setting();
        GameObject.FindGameObjectWithTag("Door").GetComponent<DoorOpen>().Open();
        monster[spawnerCount].GetComponent<MonsterState>().state = "SpawnerToDestination";
        monster[spawnerCount].GetComponent<MonsterState>().Walking();
    }
}
