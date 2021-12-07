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
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    bool CheckAniClip(string clipname)
    {
        if (this.GetComponent<Animation>().GetClip(clipname) == null)
            return false;
        else if (this.GetComponent<Animation>().GetClip(clipname) != null)
            return true;

        return false;
    }
}
