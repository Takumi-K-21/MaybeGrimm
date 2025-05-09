using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleExit : MonoBehaviour
{
    private bool exitBool;

    void Start()
    {
        exitBool = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && exitBool)
        {
            Application.Quit();
            exitBool  = false;
        }
    }
}
