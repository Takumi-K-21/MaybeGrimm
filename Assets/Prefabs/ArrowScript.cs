using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{

    private GameObject left;
    private GameObject right;

    // Start is called before the first frame update
    void Start()
    {
        left = GameObject.FindGameObjectWithTag("left");//Canvasをみつける
        right = GameObject.FindGameObjectWithTag("right");//Canvasをみつける
    }

    // Update is called once per frame
    void Update()
    {
        if (BattleScene.GameManager.isGameClear || BattleScene.GameManager.isGameOver)
        {
            left.SetActive(false);
            right.SetActive(false);
        }
    }
}
