using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateJob : MonoBehaviour
{
    public OfferTimings times;
    public MoneyPerHour money;
    public JobTitle title;

    private UISwitch ui;

    public bool generatedJobForToday = false;

    private void Start()
    {
        ui = GameObject.FindGameObjectWithTag("GameController").GetComponent<UISwitch>();
    }
    void Update()
    {


    }

    public void Generate()
    {
        ui.state = UISwitch.State.job;

        times.WorkTimes();
        money.Salary();
        title.Title();
    }
}
