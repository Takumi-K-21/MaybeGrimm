using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleButton : MonoBehaviour
{
    GameObject refObj;

    void Start()
    {
        refObj = GameObject.FindGameObjectWithTag("Bgm");
    }

    public void OnGameStart()
    {
        BgmManager attachEffect = refObj.GetComponent<BgmManager>();
        if (attachEffect != null)
        {
            attachEffect.DestroyBgm();
        }
        else
        {
            Debug.Log("Not!");
        }
        Debug.Log("A!");
    }

    public void OnSelectStage()
    {
        SceneManager.LoadScene("SelectScene");
    }
}
