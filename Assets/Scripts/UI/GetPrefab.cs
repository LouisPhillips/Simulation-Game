using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPrefab : MonoBehaviour
{
    public GameObject prefab;
    public float cost;

    private GameObject currentPlaceable;

    private bool clicked = false;
    private float mouseWheelRotation;
    public void CheckPrice()
    {
        if (cost < GobalValues.money)
        {
            Debug.Log("can buy");
            Instantiate(prefab, Input.mousePosition, prefab.transform.rotation);
            clicked = true;
        }
        else
        {
            Debug.Log("cannot buy");
        }
    }

    public void Update()
    {
        if (clicked)
        {
            HandleNewObject();

            if (currentPlaceable != null)
            {
                MoveToMouse();
                Rotate();
                ReleaseOnClick();
            }
        }
        
    }

    private bool PressedPrefab()
    {
        return currentPlaceable != null;
    }

    private void HandleNewObject()
    {
        if (PressedPrefab())
        {
            Destroy(currentPlaceable);
        }
        else
        {
            if (currentPlaceable != null)
            {
                Destroy(currentPlaceable);
            }
            currentPlaceable = Instantiate(prefab);
        }
    }

    private void MoveToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            currentPlaceable.transform.position = hit.point;
            currentPlaceable.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
        }
    }

    private void Rotate()
    {
        mouseWheelRotation += Input.mouseScrollDelta.y;
        currentPlaceable.transform.Rotate(Vector3.up, mouseWheelRotation * 10f);
    }

    private void ReleaseOnClick()
    {
        if(Input.GetMouseButtonDown(0))
        {
            currentPlaceable = null;
            clicked = false;
        }
    }
}
