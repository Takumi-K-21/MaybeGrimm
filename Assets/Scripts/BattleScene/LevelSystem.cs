using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour {
    public static  Dictionary<int, int> needExpDictionary = new Dictionary<int, int> {
        {1, 0}, {2, 3}, {3, 7}, {4, 12}, {5, 18}, {6, 25},
        {7, 33}, {8, 42}, {9, 52}, {10, 63}, {11, 75},
        {12, 88}, {13, 102}, {14, 117}, {15, 133}, {16, 150},
        {17, 168}, {18, 187}, {19, 207}, {20, 228}
    };

    public static int GetNeedForLvupExp(int lv) {
        return needExpDictionary[lv];
    }

    public static bool EndBattle() {
        if(PlayerController.lv >= 20)
        {
            return false;
        }

        PlayerController.exp += 3;

        var moreExp = GetNeedForLvupExp(PlayerController.lv + 1);

        if (moreExp <= PlayerController.exp) {
            PlayerController.lv++;
            LvText.Lv++;
            LevelUp.levelUp = true;
            return true;
        }

        return false;

    }
}
