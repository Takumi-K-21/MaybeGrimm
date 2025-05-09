using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LvText4 : MonoBehaviour
{
    public static int Lv;

    void Start()
    {
        Lv = 61;
    }

    void Update()
    {
        TextMeshProUGUI uiText = GetComponent<TextMeshProUGUI>();
        uiText.text = "Lv " + Lv;
    }
}
