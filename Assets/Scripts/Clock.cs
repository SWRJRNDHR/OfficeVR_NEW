﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* Script for clock animation. It used iTween to create animation for clock hands. */
/* Reference: https://oxmond.com/create-a-real-time-analog-wall-clock-using-c-unity-2018-intermediate-tutorial/*/

public class Clock : MonoBehaviour
{

    public GameObject secondHand;
    public GameObject minuteHand;
    public GameObject hourHand;
    string oldSeconds;

    void Update()
    {

        string seconds = System.DateTime.UtcNow.ToString("ss");


        if (seconds != oldSeconds)
        {
            UpdateTimer();
        }
        oldSeconds = seconds;
    }

    void UpdateTimer()
    {

        int secondsInt = int.Parse(System.DateTime.UtcNow.ToString("ss"));
        int minutesInt = int.Parse(System.DateTime.UtcNow.ToString("mm"));
        int hoursInt = int.Parse(System.DateTime.UtcNow.ToLocalTime().ToString("hh"));
        if (hoursInt == 12) { hoursInt = 0; }
        //print(hoursInt + " : " + minutesInt + " : " + secondsInt);

        iTween.RotateTo(secondHand, iTween.Hash("z", secondsInt * 6, "time", 1, "easetype", "easeOutQuint"));
        iTween.RotateTo(minuteHand, iTween.Hash("z", minutesInt * 6, "time", 1, "easetype", "easeOutElastic"));
        float hourDistance = (float)(minutesInt) / 60f;
        iTween.RotateTo(hourHand, iTween.Hash("z", (hoursInt + hourDistance) * 360 / 12, "time", 1, "easetype", "easeOutQuint"));

    }
}