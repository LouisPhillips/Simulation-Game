using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class SelectObject : MonoBehaviour
{
    private GameObject selection;
    private GameObject highlight;
    private RaycastHit hit;

    private SelectedInScene selectedAI;
    public GameObject popUpUI;
    private GlobalDoings globalDoings;

    private void Awake()
    {
        selectedAI = GameObject.FindGameObjectWithTag("GameController").GetComponent<SelectedInScene>();
        globalDoings = GameObject.FindGameObjectWithTag("GameController").GetComponent<GlobalDoings>();
    }
    void Update()
    {
        if (highlight != null)
        {
            highlight.gameObject.GetComponent<Outline>().enabled = false;
            highlight = null;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out hit))
        {
            highlight = hit.transform.gameObject;
            //if (highlight.CompareTag ) selectable parent on actual game object and then get component in children for location
            if (highlight.CompareTag("Selectable/AI") || highlight.CompareTag("Selectable/Object") && highlight != selection)
            {
                if (highlight.gameObject.GetComponent<Outline>() != null)
                {
                    highlight.gameObject.GetComponent<Outline>().enabled = true;
                }
                else
                {
                    Outline outline = highlight.gameObject.AddComponent<Outline>();
                    outline.enabled = true;
                    highlight.gameObject.GetComponent<Outline>().OutlineColor = Color.white;
                    highlight.gameObject.GetComponent<Outline>().OutlineWidth = 7.0f;
                }
            }
            else
            {
                highlight = null;
            }
        }

        // Selection
        if (Input.GetMouseButtonDown(0) && !globalDoings.destroying)
        {
            if (highlight)
            {
                if (selection != null)
                {
                    selection.gameObject.GetComponent<Outline>().enabled = false;
                }
                selection = hit.transform.gameObject;
                selection.gameObject.GetComponent<Outline>().enabled = true;
                highlight = null;
                Debug.Log("Pressed");
                if (selection.CompareTag("Selectable/AI"))
                {
                    PopUp("Socialize", AddToQueue.State.Social);
                }
                else if (selection.CompareTag("Selectable/Object"))
                {
                    if(selection.GetComponentInChildren<IsOccupied>().type == IsOccupied.Type.Drink)
                    {
                        PopUp("Drink", AddToQueue.State.Drink);
                    }
                    else if (selection.GetComponentInChildren<IsOccupied>().type == IsOccupied.Type.Food)
                    {
                        PopUp("Eat", AddToQueue.State.Food);
                    }
                    else if (selection.GetComponentInChildren<IsOccupied>().type == IsOccupied.Type.Sleep)
                    {
                        PopUp("Sleep", AddToQueue.State.Sleep);
                    }
                    else if (selection.GetComponentInChildren<IsOccupied>().type == IsOccupied.Type.Entertainment)
                    {
                        PopUp("Entertain", AddToQueue.State.Entertain);
                    }
                }
            }
            else
            {
                if (selection && !EventSystem.current.IsPointerOverGameObject())
                {
                    selection.gameObject.GetComponent<Outline>().enabled = false;
                    selection = null;
                    popUpUI.SetActive(false);
                }
            }
        }
    }

    void PopUp(string text, AddToQueue.State state)
    {
        
        popUpUI.SetActive(true);
        for (int i = 0; i < popUpUI.transform.childCount; i++)
        {
            popUpUI.transform.GetChild(i).position = new Vector3(Input.mousePosition.x, Input.mousePosition.y + 100);
        }
        popUpUI.GetComponentInChildren<Text>().text = text;
        popUpUI.GetComponentInChildren<AddToQueue>().state = state;
    }
}
