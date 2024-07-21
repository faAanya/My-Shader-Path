using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Clock : MonoBehaviour
{
    [SerializeField]
    Transform hoursPivot;
    [SerializeField]
    Transform minutesPivot;
    [SerializeField]
    Transform secondsPivot;

    const float hoursToDegree = -30, minSecToDegree = -6;



    // Update is called once per frame
    void Update()
    {
        TimeSpan time = DateTime.Now.TimeOfDay;
        hoursPivot.localRotation = Quaternion.Euler(0, 0, hoursToDegree * (float)time.TotalHours);
        minutesPivot.localRotation = Quaternion.Euler(0, 0, minSecToDegree * (float)time.TotalMinutes);
        secondsPivot.localRotation = Quaternion.Euler(0, 0, minSecToDegree * (float)time.TotalSeconds);
    }
}
