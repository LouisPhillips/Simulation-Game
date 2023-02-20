using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetMoney : MonoBehaviour
{
    
    void Update()
    {
        GetComponent<Text>().text = "£" + GlobalValues.money.ToString();
    }
}
