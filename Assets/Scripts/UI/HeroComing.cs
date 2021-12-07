using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeroComing : Singleton<HeroComing>
{
    public Slider slider;
    // 오고 있는 거리 (줄어들고 늘어나는 거리)
    public float comingDistance;
    float comingTime;
    int leftTime;
    // 총 거리
    float distance;
    public TextMeshProUGUI currentDistance;

    private void Start()
    {
        comingDistance = 0;
        // 10분(600초)
        distance = 600;
        comingTime = 0;
    }

    public void Update()
    {
        // 1초에 1/600 거리씩 이동 (comingDistance 늘어남)
        comingTime += Time.deltaTime * 2;
        comingDistance += Time.deltaTime * 2;
        leftTime = (int)(distance - comingDistance);
        slider.value = comingDistance / distance;

        // text
        currentDistance.text = "앞으로 남은 거리 : " + leftTime.ToString() + "km";
        
        // 거리에 따른 gameOver 여부 판별
        if (comingTime > distance) GameManager.instance.isGameOver = true;
        else GameManager.instance.isGameOver = false;
    }
}
