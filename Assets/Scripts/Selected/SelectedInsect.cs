using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedInsect : MonoBehaviour
{
    public Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out RaycastHit hitInsect))
            {
                Debug.Log(hitInsect.transform.name);

            }
        }
    }
}
