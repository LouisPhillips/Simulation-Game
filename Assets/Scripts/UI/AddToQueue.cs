using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToQueue : MonoBehaviour
{
    public enum State {Food, Drink, Sleep, Entertain, Social };
    public State state;

    private SelectedInScene selectedIn;

    private void Awake()
    {
        selectedIn = GameObject.FindGameObjectWithTag("GameController").GetComponent<SelectedInScene>();
    }
    public void AddState()
    {
        switch(state)
        {
            case State.Food:
                selectedIn.selectedAI.queueState.Add(BasePerson.State.Eating);
                break;
            case State.Drink:
                selectedIn.selectedAI.queueState.Add(BasePerson.State.Drinking);
                break;
            case State.Entertain:
                selectedIn.selectedAI.queueState.Add(BasePerson.State.Entertaining);
                break;
            case State.Sleep:
                selectedIn.selectedAI.queueState.Add(BasePerson.State.Sleeping);
                break;
            case State.Social:
                selectedIn.selectedAI.queueState.Add(BasePerson.State.GoBeSocial);
                break;
        }

        transform.parent.gameObject.SetActive(false); 
    }
}
