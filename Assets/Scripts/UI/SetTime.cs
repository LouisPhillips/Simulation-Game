using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTime : MonoBehaviour
{
    public float time;
    private TimeScaler timeScaler;
    private void Start()
    {
        timeScaler = GameObject.FindGameObjectWithTag("GameController").GetComponent<TimeScaler>();
    }

    public void SetTimeScale()
    {
        timeScaler.timeScale = time;
    }
}
