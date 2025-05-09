using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LvSlider2 : MonoBehaviour
{
    [SerializeField] private int maxExp;
    [SerializeField] private Slider gage;

    private int currentExp;

    void Start()
    {
        gage.value = 0;
    }

    void Update()
    {
        if(PlayerController2.lv >= 40)
        {
            gage.value = 0f;
            return;
        }

        currentExp = PlayerController2.exp - LevelSystem2.GetNeedForLvupExp(PlayerController2.lv);
        maxExp = LevelSystem2.GetNeedForLvupExp(PlayerController2.lv + 1) - LevelSystem2.GetNeedForLvupExp(PlayerController2.lv);

        gage.value = (float)currentExp / (float)maxExp;
    }
}
