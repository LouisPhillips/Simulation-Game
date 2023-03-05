using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollisions : MonoBehaviour
{
    public bool canBePlaced = true;

    /*private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Can be placed");
        if (collision.transform.tag != "Floor")
        {
            Debug.Log("Can be placed");
            canBePlaced = false;
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("Can be placed");
        if (collision.transform.tag != "Floor")
        {
            Debug.Log("Can be placed");
            canBePlaced = true;
        }

    }*/

    private void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.BoxCast(transform.position, transform.localScale, transform.forward, out hit, transform.rotation, 5f))
        {
            canBePlaced = false;
        }
        else
        {
            canBePlaced = true;
        }
    }
}
