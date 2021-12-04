using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterEffect : MonoBehaviour
{
    [Tooltip("0: 화상, 1: 마비, 2: 중독")]
    public GameObject[] particles;
    public GameObject effect = null;

    ///void Start()
    ///{
        ///particles = GetComponentsInChildren<GameObject>();
    ///}

    public void HideEffect()
    {
        ///if (GetComponent<MonsterState>().isSuccess)
        ///{
        effect.SetActive(false);
        effect = null;
        Debug.Log("파티클 비활성화");
        ///}
    }

    public void ShowEffect()
    {
        int randomEffect = Random.Range(0, particles.Length);
        effect = particles[randomEffect];
        effect.SetActive(true);
        Debug.Log("파티클 활성화: " + name + effect.name);
    }

    ////void OnTriggerEnter(Collider other)
    ////{
    ////    // monster가 destination에 도착하면 멈춤
    ////    if (other.tag == "Destination")
    ////    {
    ////        if (other.gameObject == GetComponent<MonsterState>().returnDestination)
    ////        {
    ////            // 랜덤 particle(effect)
    ////            int effectSelection = Random.Range(0, particle.Length);
    ////            particle[effectSelection].SetActive(true);
    ////            Debug.Log("파티클 활성화 : " + gameObject.name + particle[effectSelection].name);
    ////            effect = particle[effectSelection];
    ////        }
    ////    }
    ////}
}
