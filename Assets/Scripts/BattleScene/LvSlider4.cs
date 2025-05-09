using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LvSlider4 : MonoBehaviour
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
        currentExp = PlayerController4.exp - LevelSystem4.GetNeedForLvupExp(PlayerController4.lv);
        maxExp = LevelSystem4.GetNeedForLvupExp(PlayerController4.lv + 1) - LevelSystem4.GetNeedForLvupExp(PlayerController4.lv);

        gage.value = (float)currentExp / (float)maxExp;
    }
}
