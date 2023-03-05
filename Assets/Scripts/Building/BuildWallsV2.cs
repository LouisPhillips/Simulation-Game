using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildWallsV2 : MonoBehaviour
{
    private bool creating;

    public GameObject wallPrefab;
    public GameObject cornerPrefab;

    public GameObject start;
    public GameObject end;
    public GameObject wall;

    private bool wallClicked;

    private GlobalDoings globalDoings;

    public GameObject wallPointPrefab;
    public GameObject wallPoint;

    private void Awake()
    {
        globalDoings = GameObject.FindGameObjectWithTag("GameController").GetComponent<GlobalDoings>();
    }
    public void WallClicked()
    {
        wallClicked = true;
        wallPoint = (GameObject)Instantiate(wallPointPrefab, SnapPoint(WorldPoint()), Quaternion.identity);
    }
    private void Update()
    {
        if (wallClicked)
        {
            Marker();
            GetInput();
        }
    }

    public void GetInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SetStartPos();
            globalDoings.placing = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            SetEndPos();
            globalDoings.placing = false;
        }
        else
        {
            if (creating)
            {
                AdjustToLength();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) && globalDoings.placing)
        {
            Destroy(wall);
            Destroy(end);
            Destroy(start);
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
