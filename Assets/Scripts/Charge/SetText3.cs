using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetText3 : MonoBehaviour
{
    public GameObject chargingText;
    public GameObject MaxChargeText;

    // Update is called once per frame
    void Update()
    {
        if (PlayerController3.isCharge)
        {
            MaxChargeText.SetActive(true);
            chargingText.SetActive(false);
        }
        else
        {
            chargingText.SetActive(true);
            MaxChargeText.SetActive(false);
        }
    }
}
