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
            locations.foodLocations.Add(gameObject);
        }
        else if (transform.tag == "Drink")
        {
            locations.drinkingLocations.Add(gameObject);
        }
        else if (transform.tag == "Sleep")
        {
            locations.sleepLocations.Add(gameObject);
        }
        else if (transform.tag == "Entertainment")
        {
            locations.entertainmentLocations.Add(gameObject);
        }

    }

    
    void Update()
    {
        
    }
}
