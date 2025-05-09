using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LvSlider : MonoBehaviour
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
        if(PlayerController.lv >= 21)
        {
            gage.value = 0f;
            return;
        }

        currentExp = PlayerController.exp - LevelSystem.GetNeedForLvupExp(PlayerController.lv);
        maxExp = LevelSystem.GetNeedForLvupExp(PlayerController.lv + 1) - LevelSystem.GetNeedForLvupExp(PlayerController.lv);

        gage.value = (float)currentExp / (float)maxExp;
    }
}
