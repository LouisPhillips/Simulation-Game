using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GetTime : MonoBehaviour
{
    private TimeScaler time;
    void Start()
    {
        time = GameObject.FindGameObjectWithTag("GameController").GetComponent<TimeScaler>();
    }

    void Update()
    {
        if (time.hour < 10 && time.minutes < 10)
        {
            GetComponent<Text>().text = "0" + time.hour.ToString() + ":" + "0" + time.minutes.ToString();
        }
        if (time.hour < 10 && time.minutes >= 10)
        {
            GetComponent<Text>().text = "0" + time.hour.ToString() + ":" + time.minutes.ToString();
        }
        if (time.hour >= 10 && time.minutes < 10)
        {
            GetComponent<Text>().text = time.hour.ToString() + ":" + "0" + time.minutes.ToString();
        }
        if (time.hour >= 10 && time.minutes >= 10)
        {
            GetComponent<Text>().text = time.hour.ToString() + ":" + time.minutes.ToString();
        }

    }
}
