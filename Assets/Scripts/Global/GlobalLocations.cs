using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalLocations : MonoBehaviour
{
    [Header("Findings")]
    public List<GameObject> unocDrinkingLocations;
    public List<GameObject> unocFoodLocations;
    public List<GameObject> unocSleepLocations;
    public List<GameObject> unocEntertainmentLocations;
    public List<GameObject> workLocation;
    public List<GameObject> doorLocation;

    public List<GameObject> ocDrinkingLocations;
    public List<GameObject> ocFoodLocations;
    public List<GameObject> ocSleepLocations;
    public List<GameObject> ocEntertainmentLocations;


    void Start()
    {
        unocDrinkingLocations = new List<GameObject>();
        unocFoodLocations = new List<GameObject>();
        unocSleepLocations = new List<GameObject>();
        unocEntertainmentLocations = new List<GameObject>();

        ocDrinkingLocations = new List<GameObject>();
        ocFoodLocations = new List<GameObject>();
        ocSleepLocations = new List<GameObject>();
        ocEntertainmentLocations = new List<GameObject>();

        workLocation = new List<GameObject>();

        GameObject[] drinks = GameObject.FindGameObjectsWithTag("Drink");
        for (int i = 0; i < drinks.Length; i++)
        {
            unocDrinkingLocations.Add(drinks[i]);
        }

        GameObject[] food = GameObject.FindGameObjectsWithTag("Food");
        for (int i = 0; i < food.Length; i++)
        {
            unocFoodLocations.Add(food[i]);
        }

        GameObject[] sleep = GameObject.FindGameObjectsWithTag("Sleep");
        for (int i = 0; i < sleep.Length; i++)
        {
            unocSleepLocations.Add(sleep[i]);
        }

        GameObject[] entertainmentObj = GameObject.FindGameObjectsWithTag("Entertainment");
        for (int i = 0; i < entertainmentObj.Length; i++)
        {
            unocEntertainmentLocations.Add(entertainmentObj[i]);
        }

        GameObject[] workLocations = GameObject.FindGameObjectsWithTag("Work");
        for (int i = 0; i < workLocations.Length; i++)
        {
            workLocation.Add(workLocations[i]);
        }
    }
}
