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

    // 몬스터의 현재 상태
    MonsterState monsterState;

    private void Start()
    {
        monsters = Resources.LoadAll <GameObject> ("Monster/");
        monstersSpawner = new List<GameObject>();
        monsterState = new MonsterState();

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
            yield return new WaitForSeconds(Random.Range(1,5));
        }
    }

    void Spawn()
    {
        // 몬스터 랜덤 생성
        int selection = Random.Range(0, monsters.Length);
        GameObject monster = monstersSpawner[selection];
        monster.transform.position = transform.position;

        // 이미 활성화된 몬스터
        if (monster.activeSelf == true) return;

        // 몬스터 활성화
        if (spawnerCount < spawnerMaxSize)
        {
            monster.SetActive(true);
            Debug.Log(monster.name + "생성되었습니다");
            GameObject.FindWithTag("Door").GetComponent<DoorOpen>().Open();
            monsterState.WalkingToDestination();
            spawnerCount++;
        }
    }
}
