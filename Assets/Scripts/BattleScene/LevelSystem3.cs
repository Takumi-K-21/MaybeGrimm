using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem3 : MonoBehaviour {
    public static  Dictionary<int, int> needExpDictionary = new Dictionary<int, int> {
        {41, 0}, {42, 3}, {43, 7}, {44, 12}, {45, 18}, {46, 25},
        {47, 33}, {48, 42}, {49, 52}, {50, 63}, {51, 75},
        {52, 88}, {53, 102}, {54, 117}, {55, 133}, {56, 150},
        {57, 168}, {58, 187}, {59, 207}, {60, 228}
    };

    public static int GetNeedForLvupExp(int lv) {
        return needExpDictionary[lv];
    }

    public static bool EndBattle() {
        if(PlayerController3.lv >= 60)
        {
            return false;
        }

        PlayerController3.exp += 3;

        var moreExp = GetNeedForLvupExp(PlayerController3.lv + 1);

        if (moreExp <= PlayerController3.exp) {
            PlayerController3.lv++;
            LvText3.Lv++;
            LevelUp.levelUp = true;
            return true;
        }

        return false;

    }
}
