using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildWalls : MonoBehaviour
{
    private bool creating;

    public GameObject wallFoundationPrefab;
    public GameObject wallPrefab;

    private GameObject lastWallFound;

    private bool wallClicked;

    private GlobalDoings globalDoings;
    private void Awake()
    {
        globalDoings = GameObject.FindGameObjectWithTag("GameController").GetComponent<GlobalDoings>();
    }
    public void WallClicked()
    {
        wallClicked = true;
    }
    private void Update()
    {
        if (wallClicked)
        {
            GetInput();
        }

    }

    public void GetInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SetStartPos();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            SetEndPos();
        }
        else
        {
            if (creating)
            {
                AdjustToLength();
            }
        }
    }

    private void SetStartPos()
    {
        creating = true;
        Vector3 startPos = WorldPoint();
        startPos = SnapPoint(startPos);
        GameObject start = Instantiate(wallFoundationPrefab, startPos, Quaternion.identity);
        start.transform.position = new Vector3(startPos.x, startPos.y + 0.3f, startPos.z);
        lastWallFound = start;
    }

    private void SetEndPos()
    {
        creating = false;
    }

    private void AdjustToLength()
    {
        Vector3 current = WorldPoint();
        current = SnapPoint(current);
        // sets positions of newly created walls
        current = new Vector3(current.x, current.y + 0.3f, lastWallFound.transform.position.z); 
        if (!current.Equals(lastWallFound.transform.position))
        {
            AdjustWall(current);
        }
    }

    private void AdjustWall(Vector3 current)
    {
        GameObject newWall = Instantiate(wallFoundationPrefab, current, Quaternion.identity);
        Vector3 middle = Vector3.Lerp(newWall.transform.position, lastWallFound.transform.position, 0.5f);
        GameObject newMidWall = Instantiate(wallPrefab, middle, Quaternion.identity);
        GlobalValues.money -= 10;
        newMidWall.transform.LookAt(lastWallFound.transform);
        lastWallFound = newWall;
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
}
