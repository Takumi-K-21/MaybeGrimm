using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapExit : MonoBehaviour
{
    public SceneController scenecontroller;
    public string sceneName;
    public TitleButton titlebutton;
    public ButtonManager buttonmanager;

    bool isEscape;

    // Start is called before the first frame update
    void Start()
    {
        isEscape = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape) && isEscape)
        {
            scenecontroller.sceneChange("TitleScene");
            isEscape = false;
        }
    }
}
