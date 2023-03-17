using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildFloor : MonoBehaviour
{
    private Vector3 followPos;
    private Vector3 startPos;
    private Vector3 endPos;
    private bool buttonPressed;
    private int clickIndex = 0;

    public GameObject floor;
    public List<Vector3> tilePosition = new List<Vector3>();
    public LayerMask ground;

    public GameObject pointPrefab;
    private bool instanciated = false;

    private GameObject startPoint;
    private GameObject endPoint;
    private GameObject topRight;
    private GameObject bottomLeft;

    private GlobalDoings globalDoings;
    void Awake()
    {
        globalDoings = GameObject.FindGameObjectWithTag("GameController").GetComponent<GlobalDoings>();
    }

    public void Pressed()
    {
        buttonPressed = true;
        clickIndex += 1;
    }

    private void LateUpdate()
    {
        if (buttonPressed)
        {

            FollowMouse();
            if (Input.GetMouseButtonDown(0))
            {
                clickIndex += 1;
                startPos = followPos;
                globalDoings.placing = true;
            }
            if (Input.GetMouseButton(0))
            {
                Bounds();
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (clickIndex == 2)
                {
                    CreateFloor();
                    CleanUp();
                }
            }

            if(Input.GetKeyDown(KeyCode.Escape) && globalDoings.placing)
            {
                CleanUp();
            }
        }

    }

    private void Bounds()
    {
        if (!instanciated)
        {
            startPoint = (GameObject)Instantiate(pointPrefab, startPos, Quaternion.identity);
            endPoint = (GameObject)Instantiate(pointPrefab, endPos, Quaternion.identity);
            topRight = (GameObject)Instantiate(pointPrefab, startPos, Quaternion.identity);
            bottomLeft = (GameObject)Instantiate(pointPrefab, startPos, Quaternion.identity);

            instanciated = true;
        }
        startPoint.transform.position = startPos;
        endPoint.transform.position = followPos;
        topRight.transform.position = new Vector3(startPos.x, topRight.transform.position.y, followPos.z);
        bottomLeft.transform.position = new Vector3(followPos.x, topRight.transform.position.y, startPos.z);
    }

    private void CleanUp()
    {
        Destroy(startPoint);
        Destroy(endPoint);
        Destroy(topRight);
        Destroy(bottomLeft);
        startPoint = null;
        endPoint = null;
        topRight = null;
        bottomLeft = null;

        instanciated = false;
        buttonPressed = false;
        clickIndex = 0;
        globalDoings.placing = false;
    }

    private void FollowMouse()
    {
        RaycastHit hit;
        Ray hitpoint = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(hitpoint, out hit))
        {

            Vector3 rawPos;
            rawPos = hit.point;

            followPos.x = Mathf.Round(rawPos.x);
            followPos.y = Mathf.Round(rawPos.y);
            followPos.z = Mathf.Round(rawPos.z);

        }
    }
    private void CreateFloor()
    {
        int startX = Mathf.RoundToInt(startPos.x);
        int endX = Mathf.RoundToInt(followPos.x);
        int startZ = Mathf.RoundToInt(startPos.z);
        int endZ = Mathf.RoundToInt(followPos.z);

        if (endX < startX)
        {
            int temp = startX;
            startX = endX;
            endX = temp;
        }
        if (endZ < startZ)
        {
            int temp = startZ;
            startZ = endZ;
            endZ = temp;
        }

        for (int x = startX; x <= endX; x++)
        {
            for (int z = startZ; z <= endZ; z++)
            {
                Vector3 newTilePos = new Vector3((float)x, startPos.y, (float)z);

                if (!tilePosition.Contains(newTilePos))
                {
                    tilePosition.Add(newTilePos);

                    GameObject tile = (GameObject)Instantiate(floor, newTilePos, Quaternion.identity);
                    tile.transform.position = new Vector3(tile.transform.position.x, tile.transform.position.y + 0.35f, tile.transform.position.z);
                }
            }
        }
    }
}
