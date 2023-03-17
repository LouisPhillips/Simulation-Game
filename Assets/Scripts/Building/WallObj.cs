using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallObj : MonoBehaviour
{
    public bool wallVisible = true;
    
    void Update()
    {
        if (wallVisible)
        {
            GetComponentInChildren<Renderer>().enabled = true;
        }
        
        if (!wallVisible)
        {
            GetComponentInChildren<Renderer>().enabled = false;
        }
    }
}
