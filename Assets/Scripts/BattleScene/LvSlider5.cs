using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LvSlider5 : MonoBehaviour
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
        currentExp = PlayerController5.exp - LevelSystem5.GetNeedForLvupExp(PlayerController5.lv);
        maxExp = LevelSystem5.GetNeedForLvupExp(PlayerController5.lv + 1) - LevelSystem5.GetNeedForLvupExp(PlayerController5.lv);

        gage.value = (float)currentExp / (float)maxExp;
    }
}
