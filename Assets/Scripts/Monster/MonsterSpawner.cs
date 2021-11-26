using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    // 복제할 몬스터들
    public GameObject[] monsters;
    // 몬스터 스포너
    public List<GameObject> monstersSpawner;
    // 나타날 몬스터의 최대수
    const int spawnerMaxSize = 2;
    // 몬스터의 현재 수
    public int spawnerCount = 0;
    // 몬스터 종류의 수
    int monsterSize = 6;

    // 임시 객체
    GameObject tempObject;

    private void Start()
    {
        monsters = Resources.LoadAll <GameObject> ("Monster/");
        monstersSpawner = new List<GameObject>();

        // 스포너에 몬스터 6마리 넣어놓기
        for (int i = 0; i < monsterSize; i++)
        {
            tempObject = Instantiate(monsters[i], transform.position, transform.rotation);
            tempObject.GetComponent<MonsterDisappear>().monsterSpawner = this;
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
            Debug.Log("스폰 시작");
            Spawn();
            // 20~40초에 한 번씩 몬스터 활성화
            yield return new WaitForSeconds(Random.Range(20, 40));
        }
    }

    void Spawn()
    {
        // 몬스터 랜덤 생성
        int selection = Random.Range(0, monsters.Length);
        GameObject monster = monstersSpawner[selection];

        // 이미 활성화된 몬스터
        if (monster.activeSelf == true) return;

        monster.transform.position = transform.position;

        // 몬스터 활성화
        if (spawnerCount < spawnerMaxSize)
        {
            Debug.Log("들어감");
            monster.SetActive(true);
            GameObject.FindWithTag("Door").GetComponent<DoorOpen>().Open();
            GetComponent<MonsterState>().WalkingToDestination();
            Debug.Log(monster.name + "생성되었습니다");
            spawnerCount++;
        }
    }
}
