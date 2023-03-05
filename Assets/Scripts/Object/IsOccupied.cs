using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsOccupied : MonoBehaviour
{
    [SerializeField] private int maxUsers = 1;
    [SerializeField] private int currentUsers = 0;
    public GameObject occupiedSlot;
    public GameObject[] AIs;
    private GlobalLocations gl;
    public enum Type { Drink, Food, Sleep, Entertainment };
    public Type type;
    // Start is called before the first frame update
    void Awake()
    {
        AIs = GameObject.FindGameObjectsWithTag("AI");
        gl = GameObject.FindGameObjectWithTag("GameController").GetComponent<GlobalLocations>();
    }

    public void AddOcuppied()
    {
        switch (type)
        {
            case Type.Drink:
                gl.unocDrinkingLocations.Remove(gameObject);
                gl.ocDrinkingLocations.Add(gameObject);
                break;
            case Type.Food:
                gl.unocFoodLocations.Remove(gameObject);
                gl.ocFoodLocations.Add(gameObject);
                break;
            case Type.Sleep:
                gl.unocSleepLocations.Remove(gameObject);
                gl.ocSleepLocations.Add(gameObject);
                break;
            case Type.Entertainment:
                gl.unocEntertainmentLocations.Remove(gameObject);
                gl.ocEntertainmentLocations.Add(gameObject);
                break;
        }
    }

    public void RemoveOccupied()
    {
        occupiedSlot = null;
        switch (type)
        {
            case Type.Drink:
                gl.unocDrinkingLocations.Add(gameObject);
                gl.ocDrinkingLocations.Remove(gameObject);
                break;
            case Type.Food:
                gl.unocFoodLocations.Add(gameObject);
                gl.ocFoodLocations.Remove(gameObject);
                break;
            case Type.Sleep:
                gl.unocSleepLocations.Add(gameObject);
                gl.ocSleepLocations.Remove(gameObject);
                break;
            case Type.Entertainment:
                gl.unocEntertainmentLocations.Add(gameObject);
                gl.ocEntertainmentLocations.Remove(gameObject);
                break;
        }
    }
    // Update is called once per frame
    public void ChangeOccupationStatus()
    {
        for (int i = 0; i < AIs.Length; i++)
        {
            if (AIs[i].GetComponent<Person>().destination == transform)
            {
                occupiedSlot = AIs[i];
                switch (type)
                {
                    case Type.Drink:
                        gl.unocDrinkingLocations.Remove(gameObject);
                        gl.ocDrinkingLocations.Add(gameObject);
                        break;
                    case Type.Food:
                        gl.unocFoodLocations.Remove(gameObject);
                        gl.ocFoodLocations.Add(gameObject);
                        break;
                    case Type.Sleep:
                        gl.unocSleepLocations.Remove(gameObject);
                        gl.ocSleepLocations.Add(gameObject);
                        break;
                    case Type.Entertainment:
                        gl.unocEntertainmentLocations.Remove(gameObject);
                        gl.ocEntertainmentLocations.Add(gameObject);
                        break;
                }
            }
            else
            {
                occupiedSlot = null;
                switch (type)
                {
                    case Type.Drink:
                        gl.unocDrinkingLocations.Add(gameObject);
                        gl.ocDrinkingLocations.Remove(gameObject);
                        break;
                    case Type.Food:
                        gl.unocFoodLocations.Add(gameObject);
                        gl.ocFoodLocations.Remove(gameObject);
                        break;
                    case Type.Sleep:
                        gl.unocSleepLocations.Add(gameObject);
                        gl.ocSleepLocations.Remove(gameObject);
                        break;
                    case Type.Entertainment:
                        gl.unocEntertainmentLocations.Add(gameObject);
                        gl.ocEntertainmentLocations.Remove(gameObject);
                        break;
                }
            }
        }
    }
}
