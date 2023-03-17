using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleWalls : MonoBehaviour
{
    public int clickIndex = 0;

    private List<WallObj> walls = new List<WallObj>();
    public void OnClicked()
    {
        clickIndex += 1;
        WallObj[] wall = GameObject.FindObjectsOfType<WallObj>();
        for (int i = 0; i < wall.Length; i++)
        {
            walls.Add(wall[i]);
        }
        

        if(clickIndex > 1)
        {
            clickIndex = 0;
        }
        WallVisable();
    }

    private void WallVisable()
    {
        if (clickIndex == 0)
        {
            foreach(WallObj wallObj in walls)
            {
                wallObj.wallVisible = true;
            }
        }

        if (clickIndex == 1)
        {
            foreach (WallObj wallObj in walls)
            {
                wallObj.wallVisible = false;
            }
        }

    }
}
