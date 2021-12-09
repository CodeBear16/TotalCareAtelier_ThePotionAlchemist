using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilBehaviour : Singleton<DevilBehaviour>
{
    public int time = 0;

    private void Start()
    {
        StartCoroutine(Appear());
    }

    private IEnumerator Appear()
    {
        while (true)
        {
            // 80, 90초에 한 번 등장 (수정해야 함)
            int randomAppear = Random.Range(80, 90);
            yield return new WaitForSeconds(randomAppear);
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            PlayerBehaviour.instance.Attack();

            while(true)
            {
                yield return new WaitForSeconds(1);
                time++;
                
                if(time >= 10) gameObject.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    private bool CheckAniClip(string clipname)
    {
        if (this.GetComponent<Animation>().GetClip(clipname) == null)
            return false;
        else if (this.GetComponent<Animation>().GetClip(clipname) != null)
            return true;

        return false;
    }
}
