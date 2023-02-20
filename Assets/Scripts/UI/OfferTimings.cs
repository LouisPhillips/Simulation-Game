using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OfferTimings : MonoBehaviour
{
    public int finishTime;
    public float workLength;
    public float startTime;

    public void WorkTimes()
    {
        finishTime = Random.RandomRange(14, 20);
        workLength = Random.RandomRange(4, 12);
        startTime = finishTime - workLength;

        GetComponent<Text>().text = startTime.ToString() + ":00" + " - " + finishTime.ToString() + ":00";
    }
}
