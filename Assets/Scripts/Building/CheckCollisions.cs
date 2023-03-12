using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollisions : MonoBehaviour
{
    public bool canBePlaced = true;
    RaycastHit hit;
    public LayerMask ground;
    public bool CheckCollision()
    {

        if (Physics.BoxCast(transform.position, transform.GetComponent<BoxCollider>().size, transform.forward, out hit, transform.rotation, 2f, ground))
        {

            Debug.Log("Cant be placed");
            return false;


        }
        Debug.Log("Can be placed");
        return true;
    }

    public void TimeCheck()
    {
        Debug.Log("TIME CHECK");
        Time.timeScale = 1f;
    }


    private void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.gameObject.layer != ground)
        {
            canBePlaced = false;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.transform.gameObject.layer != ground)
        {
            canBePlaced = true;
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.transform.gameObject.layer != ground)
        {
            canBePlaced = false;
        }
    }
}
