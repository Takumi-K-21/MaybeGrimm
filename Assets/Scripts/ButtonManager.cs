using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public GameObject SeAudioSource;//���삷��Canvas�A�^�O�ŒT��

    public void OnButton()
    {
        SeManager seManager = SeManager.Instance;
        seManager.SeLoad();
        // SeAudioSource.GetComponent<SeManager>().SeLoad();//�t�F�[�h�A�E�g�t���O�𗧂Ă�
    }
}
