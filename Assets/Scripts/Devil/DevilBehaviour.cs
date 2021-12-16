using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilBehaviour : Singleton<DevilBehaviour>
{
    public int time = 0;
    public GameObject devil;

    private void OnEnable()
    {
        StartCoroutine(Appear());
    }

    private IEnumerator Appear()
    {
        while (true)
        {
            int randomAppear = Random.Range(80, 90);
            yield return new WaitForSeconds(randomAppear);
            devil.SetActive(true);
            PlayerBehaviour.instance.Attack();

            while(true)
            {
                yield return new WaitForSeconds(1);
                time++;
                
                if(time >= 10) devil.SetActive(false);
            }
        }
    }

    private bool CheckAniClip(string clipname)
    {
        if (GetComponent<Animation>().GetClip(clipname) != null)
            return true;

        return false;
    }
}
