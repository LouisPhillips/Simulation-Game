using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobDecision : MonoBehaviour
{
    public JobTitle jobTitle;
    public MoneyPerHour perHourMoney;
    public OfferTimings offerTimings;

    private GameObject gm;
    private UISwitch ui;
    private SelectedInScene person;
    public GenerateJob job;

    private void Awake()
    {
        gm = GameObject.FindGameObjectWithTag("GameController");
        ui = gm.GetComponent<UISwitch>();
        person = gm.GetComponent<SelectedInScene>();
    }
    public void AcceptJob()
    {
        person.selectedAI.jobTitle = jobTitle.jobTitle;

        person.selectedAI.paidPerHour = perHourMoney.paidPerHour;

        person.selectedAI.jobStartTime = offerTimings.startTime;
        person.selectedAI.jobFinishTime = offerTimings.finishTime;

        person.selectedAI.employment = BasePerson.Employment.Employed;

        ui.state = UISwitch.State.normal;
    }

    public void DeclineJob()
    {
        ui.state = UISwitch.State.normal;
        job.generatedJobForToday = false;
    }
}
