using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JobTitle : MonoBehaviour
{
    public string[] jobTitles = {"Engineer" , "Cleaner" , "Taxi Driver", "Delivery Driver" };
    public string jobTitle;
    public void Title()
    {
        jobTitle = jobTitles[Random.RandomRange(0, jobTitles.Length)];
        GetComponent<Text>().text = jobTitle;
    }
}
