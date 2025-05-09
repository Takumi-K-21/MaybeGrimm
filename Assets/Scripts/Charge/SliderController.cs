using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public Slider slider;

    private float currentCharge;
    private float maxCharge;

    void Start()
    {
        slider.value = 0f;
        currentCharge = 0f;
        maxCharge = 10f;
    }

    void Update()
    {
        currentCharge = PlayerController.chargeMax;
        slider.value = currentCharge / maxCharge;
    }
}
