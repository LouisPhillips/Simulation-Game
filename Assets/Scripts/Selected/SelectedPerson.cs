using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedPerson : MonoBehaviour
{
    public Camera camera;

    public Person selectedAI;

    public GameObject[] allAI;
    // Start is called before the first frame update
    void Start()
    {
        allAI = GameObject.FindGameObjectsWithTag("AI");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log(hit.transform.name);
                if (hit.transform.tag == "AI")
                {
                    for (int j = 0; j < allAI.Length; j++)
                    {
                        for (int i = 0; i < allAI[j].transform.childCount; i++)
                        {
                            allAI[j].transform.GetChild(i).gameObject.SetActive(true);
                        }
                    }
                    selectedAI = hit.transform.GetComponent<Person>();
                    Camera.main.GetComponent<CameraMovement>().target.parent = selectedAI.transform;
                    Camera.main.GetComponent<CameraMovement>().target.position = selectedAI.transform.position;
                    for (int i = 0; i < selectedAI.transform.childCount; i++)
                    {
                        selectedAI.transform.GetChild(i).gameObject.SetActive(true);
                    }
                }
            }
        }
    }
}
