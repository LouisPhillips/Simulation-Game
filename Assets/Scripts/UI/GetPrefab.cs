using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.AI;
public class GetPrefab : MonoBehaviour
{
    public GameObject prefab;
    public float cost;

    private GameObject currentPlaceable;
    private Vector3 gridSize;
    private bool clicked = false;
    private float mouseWheelRotation;

    private void Awake()
    {
        gridSize = new Vector3(1, 1, 1);
    }
    public void CheckPrice()
    {
        if (cost <= GlobalValues.money)
        {
            Debug.Log("can buy");
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
            //Destroy(currentPlaceable);
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
            Vector3 pos = currentPlaceable.transform.position;
            pos.x = Mathf.RoundToInt(pos.x / gridSize.x) * gridSize.x;
            pos.y = Mathf.RoundToInt(pos.y / gridSize.y) * gridSize.y;
            pos.z = Mathf.RoundToInt(pos.z / gridSize.z) * gridSize.z;
            currentPlaceable.transform.position = pos;
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
            GlobalValues.money -= cost;
            NavMeshBuilder.BuildNavMesh();
        }
    }
}
