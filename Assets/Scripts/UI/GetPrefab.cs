using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.AI;
public class GetPrefab : MonoBehaviour
{
    public GameObject prefab;
    public float cost;
    public LayerMask changeTo;

    private GameObject currentPlaceable;
    private Vector3 gridSize;
    private bool clicked = false;
    private float mouseWheelRotation;

    private GlobalDoings globalDoings;
    private ToggleRaycasting toggleRaycasting;
    private UISwitch ui;
    private TimeScaler timeScaler;

    private void Awake()
    {
        gridSize = new Vector3(1, 1, 1);
        globalDoings = GameObject.FindGameObjectWithTag("GameController").GetComponent<GlobalDoings>();
        ui = GameObject.FindGameObjectWithTag("GameController").GetComponent<UISwitch>();
        timeScaler = GameObject.FindGameObjectWithTag("GameController").GetComponent<TimeScaler>();
        toggleRaycasting = GameObject.FindGameObjectWithTag("GameController").GetComponent<ToggleRaycasting>();
    }
    public void CheckPrice()
    {
        if (cost <= GlobalValues.money)
        {
            globalDoings.destroying = false;
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
            globalDoings.placing = true;
            HandleNewObject();
            if (currentPlaceable != null)
            {
                MoveToMouse();
                Rotate();
                ReleaseOnClick();
                if (Input.GetKeyDown(KeyCode.Escape) && globalDoings.placing)
                {
                    Destroy(currentPlaceable);
                    currentPlaceable = null;
                    toggleRaycasting.ResetToLayer();
                    clicked = false;
                    globalDoings.placing = false;
                }
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
            toggleRaycasting.IsIgnoringRaycast();
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
        currentPlaceable.transform.Rotate(Vector3.up, mouseWheelRotation * 45f);
    }

    private void ReleaseOnClick()
    {
        timeScaler.timeScale = 0.001f;
        Time.fixedDeltaTime = 0.0001f;
        Debug.Log("Updating");
        if (currentPlaceable.GetComponent<CheckCollisions>().canBePlaced)
        {
            ui.runForCheck = true;
            if (Input.GetMouseButtonDown(0))
            {
                toggleRaycasting.ignoreObjects.Add(currentPlaceable);
                currentPlaceable = null;
                globalDoings.placing = false;
                clicked = false;
                timeScaler.timeScale = 0;
                Time.fixedDeltaTime = 1f;
                toggleRaycasting.ResetToLayer();
                GlobalValues.money -= cost;
                ui.runForCheck = false;
                NavMeshBuilder.BuildNavMesh();
            }

        }
    }

    private void ChangeToWorldObject()
    {

    }
}
