using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem2 : MonoBehaviour {
    public static  Dictionary<int, int> needExpDictionary = new Dictionary<int, int> {
        {21, 0}, {22, 3}, {23, 7}, {24, 12}, {25, 18}, {26, 25},
        {27, 33}, {28, 42}, {29, 52}, {30, 63}, {31, 75},
        {32, 88}, {33, 102}, {34, 117}, {35, 133}, {36, 150},
        {37, 168}, {38, 187}, {39, 207}, {40, 228}
    };

    public static int GetNeedForLvupExp(int lv) {
        return needExpDictionary[lv];
    }

    public static bool EndBattle() {
        if(PlayerController2.lv >= 40)
        {
            return false;
        }

        PlayerController2.exp += 3;

        var moreExp = GetNeedForLvupExp(PlayerController2.lv + 1);

        if (moreExp <= PlayerController2.exp) {
            PlayerController2.lv++;
            LvText2.Lv++;
            LevelUp.levelUp = true;
            return true;
        }

        return false;

    }
}
