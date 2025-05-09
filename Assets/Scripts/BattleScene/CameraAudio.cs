using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAudio : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    [SerializeField] AudioClip tutorialBGM;
    [SerializeField] AudioClip gameClearBGM;
    [SerializeField] AudioClip gameOverBGM;

    private float time;

    public static bool isClearBgm;

    void Start()
    {
        time = 3.0f;
        isClearBgm = true;
        audioSource.clip = tutorialBGM;
        audioSource.Play();
    }

    void Update()
    {
        time += Time.deltaTime;

        if (BattleScene.GameManager.isGameClear && isClearBgm)
        {
            audioSource.Stop();
            audioSource.clip = gameClearBGM;
            audioSource.Play();

            isClearBgm = false;
        }

        if (BattleScene.GameManager.isGameOver && isClearBgm)
        {
            audioSource.Stop();
            audioSource.clip = gameOverBGM;
            audioSource.Play();

            isClearBgm = false;
        }
    }
}
