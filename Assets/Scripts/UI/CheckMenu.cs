using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckMenu : MonoBehaviour
{
    public GameObject complitePotion;
    Potion potion;
    float timeSpan;  //경과 시간을 갖는 변수
    float[] checkTime = { 1.0f, 3.0f, 5.0f };  // 특정 시간을 갖는 변수
    float[] size = { 0.01f, 0.05f, 0.12f };
    void Start()
    {
        timeSpan = 0.0f;
        //checkTime = 5.0f;  // 특정시간을 5초로 지정
    }
    public void check()
    {
        Debug.Log("ㅎ2");
    }

    public void putWaterCheck()
    {
        Debug.Log("현재 닿은시간 :" + timeSpan);
        timeSpan += Time.deltaTime;  // 경과 시간을 계속 등록
       
        // 시간경과에 따라 항아리속 물크기 증가
        for (int i = 0; i < checkTime.Length; i++)
        {
            if (timeSpan > checkTime[i])  // 경과 시간이 특정 시간이 보다 커졋을 경우
            {
                complitePotion.transform.localScale = new Vector3(0.35f, size[i], 0.35f);

                /*
                  항아리 안의 물크기 체크 후 
                */
                if (i == 2)
                {
                    timeSpan = 0;
                    SceneManager.LoadScene("_Scene");
                }
            }
        }
    }
}
