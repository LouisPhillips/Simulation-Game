using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Person : BasePerson
{
    float time = 0;
    // Start is called before the first frame update
    public override void Start()
    {
        maxTiredness = 100;
        maxHunger = 100;
        maxThirst = 100;
        maxEntertained = 100;
        maxSocial = 100;

        satisfiedTiredness = 40;
        satisfiedHunger = 70;
        satisfiedThirst = 70;
        satisfiedEntertainment = 50;
        satisfiedSocial = 50;

        criticalTiredness = 20;
        criticalHunger = 20;
        criticalThirst = 20;
        criticalEntertainment = 20;
        criticalSocial = 20;

        hungerInteractionTime = 20f;
        thirstInteractionTime = 20f;
        sleepInteractionTime = 100f;
        entertainmentInteractionTime = 40f;
        socialInteractionTime = 30f;


        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }
}
