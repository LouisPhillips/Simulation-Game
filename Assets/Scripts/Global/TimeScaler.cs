using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaler : MonoBehaviour
{
    public int hour = 8;
    public int minutes = 0;
    public int day = 0;
    [Space(10)]
    private float countSecond = 0f;
    private float second = 0.2f;
    public float timeScale;
    void Update()
    {
        Time.timeScale = timeScale;

        countSecond += Time.deltaTime;
        if (countSecond > second)
        {
            minutes += 1;
            countSecond = 0f;
        }

        if (minutes > 59)
        {
            minutes = 0;
            hour += 1;

        }

        if (hour > 23)
        {
            hour = 0;
            day += 1;
        }
    }
}
