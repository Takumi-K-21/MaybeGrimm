using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//シーン切り替えに使用するライブラリ
using UnityEngine.Video;

public class SceneChange : MonoBehaviour
{
    public SceneController scenecontroller;
    public string sceneName;
    private bool isPress;

    [SerializeField]
    VideoPlayer videoPlayer;

    public GameObject _text;
    public GameObject _text2;

    // Start is called before the first frame update
    void Start()
   {
        videoPlayer.loopPointReached += LoopPointReached;
        isPress = false;
        videoPlayer.Play();
    }
 
   // Update is called once per frame
   void Update()
   {
       if (Input.GetMouseButtonDown(0) && !isPress) {
            scenecontroller.sceneChange(sceneName);
            isPress = true;
        }
    }

    public void LoopPointReached(VideoPlayer vp)
    {
        scenecontroller.sceneChange("TitleScene");
    }
}
