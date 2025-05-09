using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem4 : MonoBehaviour {
    public static  Dictionary<int, int> needExpDictionary = new Dictionary<int, int> {
        {61, 0}, {62, 3}, {63, 7}, {64, 12}, {65, 18}, {66, 25},
        {67, 33}, {68, 42}, {69, 52}, {70, 63}, {71, 75},
        {72, 88}, {73, 102}, {74, 117}, {75, 133}, {76, 150},
        {77, 168}, {78, 187}, {79, 207}, {80, 228}
    };

    public static int GetNeedForLvupExp(int lv) {
        return needExpDictionary[lv];
    }

    public static bool EndBattle() {
        if (PlayerController4.lv >= 80)
        {
            return false;
        }

        PlayerController4.exp += 3;

        var moreExp = GetNeedForLvupExp(PlayerController4.lv + 1);

        if (moreExp <= PlayerController4.exp) {
            PlayerController4.lv++;
            LvText4.Lv++;
            LevelUp.levelUp = true;
            return true;
        }

        return false;

    }
}
