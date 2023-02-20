using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugScript : MonoBehaviour
{
    private GameObject[] ais;
    private void Start()
    {
        ais = GameObject.FindGameObjectsWithTag("AI");
    }
    public void KillHunger()
    {
        for (int i = 0; i < ais.Length; i++)
        {
            ais[i].GetComponent<Person>().hunger = 30;
        }
    }
    public void KillThirst()
    {
        for (int i = 0; i < ais.Length; i++)
        {
            ais[i].GetComponent<Person>().thirst = 30;
        }
    }

    public void KillSleep()
    {
        for (int i = 0; i < ais.Length; i++)
        {
            ais[i].GetComponent<Person>().tiredness = 30;
        }
    }

    public void KillEntertainment()
    {
        for (int i = 0; i < ais.Length; i++)
        {
            ais[i].GetComponent<Person>().entertainment = 30;
        }
    }
}
