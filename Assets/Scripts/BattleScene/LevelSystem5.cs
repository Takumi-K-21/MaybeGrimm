using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem5 : MonoBehaviour {
    public static  Dictionary<int, int> needExpDictionary = new Dictionary<int, int> {
        {81, 0}, {82, 3}, {83, 7}, {84, 12}, {85, 18}, {86, 25},
        {87, 33}, {88, 42}, {89, 52}, {90, 63}, {91, 75},
        {92, 88}, {93, 102}, {94, 117}, {95, 133}, {96, 150},
        {97, 168}, {98, 187}, {99, 207}, {100, 228}
    };

    public static int GetNeedForLvupExp(int lv) {
        return needExpDictionary[lv];
    }

    public static bool EndBattle() {
        if (PlayerController5.lv >= 100)
        {
            return false;
        }

        PlayerController5.exp += 3;

        var moreExp = GetNeedForLvupExp(PlayerController5.lv + 1);

        if (moreExp <= PlayerController5.exp) {
            PlayerController5.lv++;
            LvText5.Lv++;
            LevelUp.levelUp = true;
            return true;
        }

        return false;

    }
}
