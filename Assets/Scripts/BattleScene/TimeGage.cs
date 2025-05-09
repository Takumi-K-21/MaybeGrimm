using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeGage : MonoBehaviour {
    [SerializeField] private float maxTime = 150;
    [SerializeField] private Slider gage;

    public static float currentTime;

    void Start() {
        gage.value = 1;

        currentTime = maxTime;
    }

    void Update() {
        currentTime -= Time.deltaTime ;

        gage.value = currentTime / maxTime;
    }
}
