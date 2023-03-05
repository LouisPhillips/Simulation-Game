using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsVisible : MonoBehaviour
{
    private SelectedInScene selectedPerson;
    public GameObject lookForJob;
    void Start()
    {
        selectedPerson = GameObject.FindGameObjectWithTag("GameController").GetComponent<SelectedInScene>();
    }

    // Update is called once per frame
    void Update()
    {
        if (selectedPerson.selectedAI.employment == BasePerson.Employment.Unemployed)
        {
            lookForJob.SetActive(true);
        }
        else
        {
            lookForJob.SetActive(false);
        }
    }
}
