using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsOccupied : MonoBehaviour
{
    public GameObject occupiedSlot;
    public GameObject[] AIs;
    // Start is called before the first frame update
    void Start()
    {
        AIs = GameObject.FindGameObjectsWithTag("AI");
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < AIs.Length; i++)
        {
            if (AIs[i].GetComponent<Person>().destination == transform)
            {
                occupiedSlot = AIs[i];
            }
            else
            {
                occupiedSlot = null;
            }
        }
    }
}
