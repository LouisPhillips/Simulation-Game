using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Person : BasePerson
{
    // Start is called before the first frame update
    public override void Start()
    {
        maxTiredness = 100;
        maxHunger = 100;
        maxThirst = 100;
        maxHealth = 250;
        maxEntertained = 100;

        satisfiedTiredness = 80;
        satisfiedHunger = 75;
        satisfiedThirst = 75;
        satisfiedHealth = 100;
        satisfiedEntertainment = 75;
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }
}
