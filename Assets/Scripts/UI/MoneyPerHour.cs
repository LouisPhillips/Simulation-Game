using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyPerHour : MonoBehaviour
{
    public int paidPerHourMin = 9;
    public int paidPerHourMax = 22;
    public int paidPerHour;

    public void Salary()
    {
        paidPerHour = Random.RandomRange(paidPerHourMin, paidPerHourMax);

        GetComponent<Text>().text = "£" + paidPerHour.ToString() + " per hour";
    }
}
