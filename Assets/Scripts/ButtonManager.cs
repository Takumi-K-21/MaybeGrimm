using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public GameObject SeAudioSource;//操作するCanvas、タグで探す

    public void OnButton()
    {
        SeManager seManager = SeManager.Instance;
        seManager.SeLoad();
        // SeAudioSource.GetComponent<SeManager>().SeLoad();//フェードアウトフラグを立てる
    }
}
