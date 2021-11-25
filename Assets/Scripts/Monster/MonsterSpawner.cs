using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    // 복제할 몬스터들
    GameObject[] monsters;
    // 몬스터 스포너
    public List<GameObject> monstersSpawner;
    // 나타날 몬스터의 최대수
    const int spawnerMaxSize = 5;
    int monsterSize = 12;
    // 몬스터의 현재 수
    public static int spawnerCount = 0;
    // 몬스터 생성 위치
    public static Transform spawnerPosition;
    // 임시 객체
    GameObject tempObject;

    private void Start()
    {
        spawnerPosition = gameObject.transform;
        monsters = Resources.LoadAll <GameObject> ("Monster/");
        monstersSpawner = new List<GameObject>();

        // 스포너에 몬스터 12마리 넣어놓기
        for (int i = 0; i < monsterSize; i++)
        {
            tempObject = Instantiate(monsters[i], spawnerPosition.position, spawnerPosition.rotation);
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
            yield return new WaitForSeconds(Random.Range(1, 5));
        }
    }

    void Spawn()
    {
        Debug.Log("스폰 됨");

        // 몬스터 랜덤 생성
        int selection = Random.Range(0, monsters.Length);
        GameObject monster = monstersSpawner[selection];

        // 이미 활성화된 몬스터
        if (monster.activeSelf == true) return;

        monster.transform.position = spawnerPosition.position;
        monster.transform.rotation = spawnerPosition.rotation;

        // 몬스터 활성화
        if (spawnerCount < spawnerMaxSize)
        {
            Debug.Log("들어감");
            monster.SetActive(true);
            Debug.Log(monster.name + "생성되었습니다");
            spawnerCount++;
        }
    }
}
