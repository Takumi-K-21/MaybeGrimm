using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LvText2 : MonoBehaviour
{
    public static int Lv;

    void Start()
    {
        Lv = 21;
    }

    void Update()
    {
        TextMeshProUGUI uiText = GetComponent<TextMeshProUGUI>();
        uiText.text = "Lv " + Lv;
    }
}
