using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.AI;
public class BuildWallsV2 : MonoBehaviour
{
    public bool creating;

    public GameObject wallPrefab;
    public GameObject cornerPrefab;

    public GameObject start;
    public GameObject end;
    public GameObject wall;

    public bool wallClicked;

    private GlobalDoings globalDoings;

    public GameObject wallPointPrefab;
    public GameObject wallPoint;

    private TimeScaler timeScaler;
    private ToggleRaycasting toggleRaycasting;
    private UISwitch ui;

    private void Awake()
    {
        timeScaler = GameObject.FindGameObjectWithTag("GameController").GetComponent<TimeScaler>();
        globalDoings = GameObject.FindGameObjectWithTag("GameController").GetComponent<GlobalDoings>();
        toggleRaycasting = GameObject.FindGameObjectWithTag("GameController").GetComponent<ToggleRaycasting>();
        ui = GameObject.FindGameObjectWithTag("GameController").GetComponent<UISwitch>();
    }
    public void WallClicked()
    {
        wallClicked = true;
        wallPoint = (GameObject)Instantiate(wallPointPrefab, SnapPoint(WorldPoint()), Quaternion.identity);
    }
    public void Update()
    {
        if (wallClicked)
        {
            timeScaler.timeScale = 0.001f;
            Time.fixedDeltaTime = 0.0001f;
            Debug.Log("Times  :  " + timeScaler.timeScale + "  :  " + Time.fixedDeltaTime);
            Marker();
            GetInput();
        }
    }

    public void GetInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            toggleRaycasting.IsIgnoringRaycast();
            SetStartPos();
            globalDoings.placing = true;
        }
        if (creating)
        {
            AdjustToLength();
        }


        if (wall.GetComponentInChildren<CheckCollisions>().canBePlaced)
        {
            ui.runForCheck = true;
            if (Input.GetMouseButtonUp(0))
            {
                SetEndPos();
                globalDoings.placing = false;
                timeScaler.timeScale = 0;
                Time.fixedDeltaTime = 1f;
                toggleRaycasting.ignoreObjects.Add(wall);
                toggleRaycasting.ResetToLayer();
                ui.runForCheck = false;
                NavMeshBuilder.BuildNavMesh();

            }
        }
        else
        {
            Debug.Log("Cannot place");
            if (Input.GetMouseButtonUp(0))
            {
                Destroy(wall);
                Destroy(end);
                Destroy(start);
                wall = null;
                end = null;
                start = null;
                timeScaler.timeScale = 0;
                Time.fixedDeltaTime = 1f;
                toggleRaycasting.ResetToLayer();
                globalDoings.placing = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) && globalDoings.placing)
        {
            //creating = false;

            Destroy(wall);
            Destroy(end);
            Destroy(start);
            wall = null;
            end = null;
            start = null;
            timeScaler.timeScale = 0;
            Time.fixedDeltaTime = 1f;
            toggleRaycasting.ResetToLayer();
            globalDoings.placing = false;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !creating)
        {
            globalDoings.placing = false;
            wallClicked = false;
            timeScaler.timeScale = 0;
            Time.fixedDeltaTime = 1f;
        }
    }

    private void SetStartPos()
    {
        creating = true;
        Vector3 startPos = WorldPoint();
        startPos = SnapPoint(startPos);
        start = (GameObject)Instantiate(cornerPrefab, startPos, Quaternion.identity);
        start.transform.position = new Vector3(startPos.x, startPos.y + 0.3f, startPos.z);
        wall = (GameObject)Instantiate(wallPrefab, start.transform.position, Quaternion.identity);
        end = (GameObject)Instantiate(cornerPrefab, startPos, Quaternion.identity);
    }

    private void SetEndPos()
    {
        creating = false;
        Vector3 endPos = WorldPoint();
        endPos = SnapPoint(endPos);
        end.transform.position = new Vector3(endPos.x, endPos.y + 0.3f, endPos.z);
        if (wall.transform.localScale.z > 1)
        {
            GlobalValues.money -= Mathf.Round(10 * wall.transform.localScale.z) - 1;
        }

    }

    private void AdjustToLength()
    {
        Vector3 current = WorldPoint();
        current = SnapPoint(current);
        end.transform.position = new Vector3(current.x, current.y + 0.3f, current.z);
        AdjustWall();
    }

    private void AdjustWall()
    {
        start.transform.LookAt(end.transform.position);
        end.transform.LookAt(start.transform.position);
        float distance = Vector3.Distance(start.transform.position, end.transform.position);
        wall.transform.position = start.transform.position + distance / 2 * start.transform.forward.normalized;
        wall.transform.rotation = start.transform.rotation;
        wall.transform.localScale = new Vector3(wall.transform.localScale.x, wall.transform.localScale.y, distance * 1.1f);
    }

    private Vector3 WorldPoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            return hit.point;
        }
        return Vector3.zero;
    }

    private Vector3 SnapPoint(Vector3 original)
    {
        Vector3 snapped;
        snapped.x = Mathf.Floor(original.x);
        snapped.y = Mathf.Floor(original.y);
        snapped.z = Mathf.Floor(original.z);
        return snapped;
    }

    private void Marker()
    {
        if (!globalDoings.placing)
        {
            wallPoint.transform.position = SnapPoint(WorldPoint());
        }
        else
        {
            wallPoint.transform.position = new Vector3(SnapPoint(WorldPoint()).x, 0.1f, SnapPoint(WorldPoint()).z);
        }
    }


}
