using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeValue : MonoBehaviour
{
    public Slider slider;
    public enum State { Hunger, Thrist, Entertainment, Sleep};
    public State state;
    public GameObject AI;
    private Person person;

    private void Start()
    {
        slider = GetComponent<Slider>();
        person = AI.GetComponent<Person>();
    }
    public void ChangeSliderValue()
    {
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
