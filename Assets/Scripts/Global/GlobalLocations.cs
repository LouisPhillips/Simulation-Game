using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalLocations : MonoBehaviour
{
    [Header("Findings")]
    public List<GameObject> drinkingLocations;
    public List<GameObject> foodLocations;
    public List<GameObject> sleepLocations;
    public List<GameObject> entertainmentLocations;

    public List<GameObject> doorLocation;
    void Start()
    {
        drinkingLocations = new List<GameObject>();
        foodLocations = new List<GameObject>();
        sleepLocations = new List<GameObject>();
        entertainmentLocations = new List<GameObject>();

        GameObject[] drinks = GameObject.FindGameObjectsWithTag("Drink");
        for (int i = 0; i < drinks.Length; i++)
        {
            drinkingLocations.Add(drinks[i]);
        }

        GameObject[] food = GameObject.FindGameObjectsWithTag("Food");
        for (int i = 0; i < food.Length; i++)
        {
            foodLocations.Add(food[i]);
        }

        GameObject[] sleep = GameObject.FindGameObjectsWithTag("Sleep");
        for (int i = 0; i < sleep.Length; i++)
        {
            sleepLocations.Add(sleep[i]);
        }

        GameObject[] entertainmentObj = GameObject.FindGameObjectsWithTag("Entertainment");
        for (int i = 0; i < entertainmentObj.Length; i++)
        {
            entertainmentLocations.Add(entertainmentObj[i]);
        }
    }
}
