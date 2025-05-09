using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyGage : MonoBehaviour {
    [SerializeField] private float maxHp = 500;
    [SerializeField] private Slider gage;

    public static float currentHp;

    void Start() {
        gage.value = 1;

        currentHp = maxHp;
    }

    void Update() {
        gage.value = currentHp / maxHp;
        Debug.Log(currentHp);
    }
}
