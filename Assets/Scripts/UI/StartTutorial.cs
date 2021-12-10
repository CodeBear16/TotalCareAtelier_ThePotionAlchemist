using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTutorial : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Time());
    }
   
    private IEnumerator Time()
    {
        yield return new WaitForSeconds(60);
        Destroy(gameObject);
    }
}
