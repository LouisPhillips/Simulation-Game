using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyObjects : MonoBehaviour
{
    public LayerMask desctructionLayer;

    public GameObject toDestroy;
    public Material destructionMaterial;
    private GlobalDoings globalDoings;
    private bool clicked = false;

    public int clickIndex = 0;

    private void Awake()
    {
        globalDoings = GameObject.FindGameObjectWithTag("GameController").GetComponent<GlobalDoings>();
    }
    public void DestroyObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, desctructionLayer))
            {
                Destroy(hit.transform.gameObject);
            }
        }
    }

    public void Update()
    {
        if (globalDoings.destroying)
        {
            DestroyObject();
        }

        if (clickIndex == 1)
        {
            globalDoings.destroying = true;
            GetComponent<Image>().color = Color.red;
        }
        else
        {
            globalDoings.destroying = false;
            GetComponent<Image>().color = Color.green;

        }

        if (clickIndex > 1)
        {
            clickIndex = 0;
        }
        
    }

    public void ClickedButton()
    {
        clickIndex += 1;
    }


}
