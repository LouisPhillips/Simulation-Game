using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToTaskList : MonoBehaviour
{
    public GameObject prefab;
    private SelectedInScene selectedAI;
    public bool addedToList;
    private int lastInt;
    private Person lastSelected;
    void Awake()
    {
        selectedAI = GameObject.FindGameObjectWithTag("GameController").GetComponent<SelectedInScene>();

        lastInt = selectedAI.selectedAI.queueState.Count;
        lastSelected = selectedAI.selectedAI;
    }

    private void Update()
    {
        if (selectedAI.selectedAI.queueState.Count != lastInt)
        {
            AddToQueue();
            lastInt = selectedAI.selectedAI.queueState.Count;
        }

        if (selectedAI.selectedAI != lastSelected)
        {
            AddToQueue();
            lastSelected = selectedAI.selectedAI;
        }
    }

    public void AddToQueue()
    {
        // Resets visual queue
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
     
        // adds elements back in
        for (int i = 0; i < selectedAI.selectedAI.queueState.Count; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.transform.SetParent(this.gameObject.transform, false);
            Debug.Log("Added" + selectedAI.selectedAI.queueState[i]);
        }
    }
}
