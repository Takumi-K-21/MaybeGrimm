using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LvSlider3 : MonoBehaviour
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
        if(PlayerController3.lv >= 60)
        {
            gage.value = 0f;
            return;
        }

        currentExp = PlayerController3.exp - LevelSystem3.GetNeedForLvupExp(PlayerController3.lv);
        maxExp = LevelSystem3.GetNeedForLvupExp(PlayerController3.lv + 1) - LevelSystem3.GetNeedForLvupExp(PlayerController3.lv);

        gage.value = (float)currentExp / (float)maxExp;
    }
}
