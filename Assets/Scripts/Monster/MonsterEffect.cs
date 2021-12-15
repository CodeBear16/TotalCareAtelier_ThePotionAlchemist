using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterEffect : MonoBehaviour
{
    [Tooltip("0: 화상, 1: 마비, 2: 중독")]
    public GameObject[] particles;
    public GameObject effect = null;


    public void HideEffect()
    {
        effect.SetActive(false);
        effect = null;
        Debug.Log("파티클 비활성화");
    }

    public void ShowEffect()
    {
        int randomEffect = Random.Range(0, particles.Length);
        effect = particles[randomEffect];
        effect.SetActive(true);
        Debug.Log("파티클 활성화: " + name + effect.name);
    }

}
