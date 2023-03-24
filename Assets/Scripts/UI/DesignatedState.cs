using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesignatedState : MonoBehaviour
{
    public BasePerson.State state;
    private SelectedInScene selectedAI;

    private void Awake()
    {
        selectedAI = GameObject.FindGameObjectWithTag("GameController").GetComponent<SelectedInScene>();
        /*for (int i = 0; i < selectedAI.selectedAI.queueState.Count; i++)
        {
            state = selectedAI.selectedAI.queueState[i];
        }*/
    }

    public void RemoveAtQueue()
    {
        for (int i = 0; i < selectedAI.selectedAI.queueState.Count; i++)
        {
            selectedAI.selectedAI.queueState.Remove(state);
            if (selectedAI.selectedAI.queueState.Count == 0)
            {
                selectedAI.selectedAI.queueState.Add(BasePerson.State.Wander);
            }
        }
    }
}
