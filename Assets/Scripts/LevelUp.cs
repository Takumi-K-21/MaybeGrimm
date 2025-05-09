using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUp : MonoBehaviour
{
    public GameObject _text;
    public static bool levelUp;
    private float time;

    void Start()
    {
        levelUp = false;
        time = 0f;
    }

    void Update()
    {
        Debug.Log("NOT LEVEL UP!");

        if (levelUp)
        {
            _text.SetActive(true);
            time += Time.deltaTime;

            Debug.Log("LEVEL UP!");

            if(time > 1f) {
                levelUp = false;
                _text.SetActive(false);
                time = 0f;
            }
        }
    }
}
