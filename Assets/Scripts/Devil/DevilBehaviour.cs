using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilBehaviour : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Appear());
    }

    IEnumerator Appear()
    {
        while (true)
        {
            int randomAppear = Random.Range(1, 5);
            yield return new WaitForSeconds(randomAppear);
            GameObject.Find("Devil").transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
