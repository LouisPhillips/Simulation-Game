using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleRaycasting : MonoBehaviour
{
    public List<GameObject> ignoreObjects;

    public LayerMask ignoreRaycast;
    public LayerMask placeable;
    public LayerMask ui;
    public void IsIgnoringRaycast(bool boolean)
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
            if (boolean)
            {
                obj.layer = ignoreRaycast;
            }
            else
            {
                if (obj.transform.tag == ("AI"))
                {
                    obj.layer = ui;
                }
                else
                {
                    obj.layer = placeable;
                }

            }
        }

    }
}
