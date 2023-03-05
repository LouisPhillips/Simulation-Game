using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToList : MonoBehaviour
{
    private GlobalLocations locations;
    void Awake()
    {
        locations = GameObject.FindGameObjectWithTag("GameController").GetComponent<GlobalLocations>();

        if (transform.tag == "Food")
        {
            locations.unocFoodLocations.Add(gameObject);
        }
        else if (transform.tag == "Drink")
        {
            locations.unocDrinkingLocations.Add(gameObject);
        }
        else if (transform.tag == "Sleep")
        {
            locations.unocSleepLocations.Add(gameObject);
        }
        else if (transform.tag == "Entertainment")
        {
            locations.unocEntertainmentLocations.Add(gameObject);
        }

    }

    
    void Update()
    {
        
    }
}
