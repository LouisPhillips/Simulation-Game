using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeValue : MonoBehaviour
{
    public Slider slider;
    public enum State { Hunger, Thrist, Entertainment, Sleep};
    public State state;
    public Person person;
    public SelectedPerson selectedPerson;

    private void Start()
    {
        slider = GetComponent<Slider>();

       
    }
    public void ChangeSliderValue()
    {
        selectedPerson = GameObject.FindGameObjectWithTag("GameController").GetComponent<SelectedPerson>();
        person = selectedPerson.selectedAI;

        switch (state)
        {
            case State.Hunger:
                slider.value = person.hunger;
                break;
            case State.Thrist:
                slider.value = person.thirst;
                break;
            case State.Entertainment:
                slider.value = person.entertainment;
                break;
            case State.Sleep:
                slider.value = person.tiredness;
                break;
        }
    }

    private void Update()
    {
        ChangeSliderValue();
    }
}
