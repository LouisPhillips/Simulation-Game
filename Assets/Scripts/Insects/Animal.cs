using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Animal : Insect
{
    // Start is called before the first frame update
    public override void Start()
    {
        maxTiredness = 100;
        maxHunger = 100;
        maxThirst = 100;
        maxHealth = 250;

        satisfiedTiredness = 80;
        satisfiedHunger = 75;
        satisfiedThirst = 75;
        satisfiedHealth = 100;

        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }
}
