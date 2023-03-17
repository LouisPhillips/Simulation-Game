using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleRaycasting : MonoBehaviour
{
    public List<GameObject> ignoreObjects;

    public LayerMask ignoreRaycast;
    public LayerMask placeable;
    public LayerMask ui;

    private bool added = false;

    private void Update()
    {

    }

    public void IsIgnoringRaycast()
    {
        if (!added)
        {
            GameObject[] playerRay = GameObject.FindGameObjectsWithTag("AI");
            for (int i = 0; i < playerRay.Length; i++)
            {
                ignoreObjects.Add(playerRay[i]);
            }
            GameObject[] objectRay = GameObject.FindGameObjectsWithTag("Selectable/Object");
            for (int i = 0; i < objectRay.Length; i++)
            {
                ignoreObjects.Add(objectRay[i]);
            }

            foreach (GameObject obj in ignoreObjects)
            {
                obj.layer = 2;
                
            }
            added = true;
        }
    }

    public void ResetToLayer()
    {
        foreach (GameObject obj in ignoreObjects)
        {
            if (obj.transform.tag == ("AI"))
            {
                obj.layer = 5;
            }
            else
            {
                obj.layer = 3;
            }
            added = false;
        }
    }
}
