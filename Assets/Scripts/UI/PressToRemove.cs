using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PressToRemove : MonoBehaviour
{
    private SelectedInScene selectedAI;
    private void Awake()
    {
        selectedAI = GameObject.FindGameObjectWithTag("GameController").GetComponent<SelectedInScene>();

    }

    private void Update()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Button>().onClick.AddListener(RemoveAtIndex);
        }
        
    }
    public void RemoveAtIndex()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Debug.Log("Remvoed" + " " + selectedAI.selectedAI.queueState[i]);
            selectedAI.selectedAI.queueState.RemoveAt(i);
            //Destroy(transform.GetChild(i).gameObject);
            if(selectedAI.selectedAI.queueState.Count == 0)
            {
                selectedAI.selectedAI.queueState.Add(BasePerson.State.Wander);
            }
        }
    }
}
