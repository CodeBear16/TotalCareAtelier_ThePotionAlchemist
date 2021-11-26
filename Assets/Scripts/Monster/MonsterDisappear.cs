using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDisappear : MonoBehaviour
{
    public MonsterSpawner monsterSpawner;

    private void OnEnable()
    {
        StartCoroutine(Disappear());
    }
    IEnumerator Disappear()
    {
           while(true)
         {
            yield return new WaitForSeconds(2);
            if (Vector3.Distance(monsterSpawner.transform.position, transform.position) < 0.05f)
            {
                gameObject.SetActive(false);
                Debug.Log(gameObject.name + "이 비활성화 되었습니다");
                monsterSpawner.spawnerCount--;
            }
        }

    }
}
