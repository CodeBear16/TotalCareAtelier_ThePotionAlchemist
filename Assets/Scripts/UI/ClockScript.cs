using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class ClockScript : MonoBehaviour
{
    DateTime dateTime;
    string hour;
    string minute;
    string second;
    public TextMeshProUGUI timeText;

    private void Update()
    {
        dateTime = DateTime.Now;
        hour = dateTime.Hour.ToString();
        minute = dateTime.Minute.ToString();
        second = dateTime.Second.ToString();

        timeText.text = hour + " : " + minute + " : " + second;
    }
}
