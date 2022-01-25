using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : Singleton<MonsterSpawner>
{
    // 도착지 리스트
    public static List<GameObject> destinationList = new List<GameObject>();
    // 가능한 도착지의 리스트
    public List<int> availableDestination = new List<int>();
    // 이동할 도착지
    MonsterDestination destination;
    // 복제할 몬스터들
    public GameObject[] monsterPrefabs;
    // 몬스터 스포너에서 나온 몬스터
    public List<GameObject> monsterPool = new List<GameObject>();
    // 나타날 몬스터의 최대수
    const int spawnerMaxSize = 50;
    // 몬스터의 현재 수
    public int spawnCount = 0;
    // 임시 객체
    GameObject tempObject;

    void Start()
    {
        monsterPrefabs = Resources.LoadAll<GameObject>("Monster/");

        // 몬스터들의 목적지 리스트 설정
        destinationList.AddRange(GameObject.FindGameObjectsWithTag("Destination"));

        // 스포너에 있는 몬스터들 (50마리 미리 넣어놓는 것)
        while (monsterPool.Count < spawnerMaxSize)
        {
            int selection = Random.Range(0, monsterPrefabs.Length);
            // 몬스터를 스포너의 자식 오브젝트로 넣기
            tempObject = Instantiate(monsterPrefabs[selection], transform);
            ReturnToSpawner(tempObject);
            monsterPool.Add(tempObject);
        }
        
        StartCoroutine(SpawnCoroutine());
    }

    IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            Spawn();
            // 20~30초에 한 번씩 몬스터 활성화
            yield return new WaitForSeconds(Random.Range(20, 30));
        }
    }
   
    public void Spawn()
    {
        CheckDestination();
        if (availableDestination.Count == 0)
            return;
        // 매번 바뀌는 도착지 리스트에 초기화해준다.
        availableDestination.Clear();

        spawnCount++;
        // spawnCount 가 50보다 커지면 다시 처음으로 돌아가게 한다.
        if (spawnCount > monsterPool.Count)
            spawnCount = 0;

        tempObject = monsterPool[spawnCount];

        // 이미 활성화된 몬스터라면 
        if (tempObject.activeSelf == true)
        {
            spawnCount++;
            return;
        }

        // 몬스터 활성화
        tempObject.SetActive(true);
        Debug.Log(tempObject.name + "가 출현했습니다.");

        // 도착지 지정
        destination.Occupy();
        tempObject.GetComponent<MonsterState>().Setting(destination.gameObject);
    }

    public void CheckDestination()
    {
        for (int i = 0; i < destinationList.Count; i++)
        {
            destination = destinationList[i].GetComponent<MonsterDestination>();
            if (destination.isOccupied == false)
                availableDestination.Add(i);
        }
        if (availableDestination.Count == 0)
            return;

        int random = Random.Range(0, availableDestination.Count);
        destination = destinationList[availableDestination[random]].GetComponent<MonsterDestination>();
    }

    public void ReturnToSpawner(GameObject tempObject)
    {
        tempObject.transform.SetPositionAndRotation(transform.position, transform.rotation);
        tempObject.SetActive(false);
    }
} 
