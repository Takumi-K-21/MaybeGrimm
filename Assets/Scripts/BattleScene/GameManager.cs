using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleScene
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] AudioSource audioSource;
        [SerializeField] AudioSource audioSource2;

        public GameObject playUI;
        public GameObject resultUI;
        public GameObject GameClearUI;

        public static bool isGameResume;
        public static bool isGameClear;
        public static bool isGameOver;

        bool flag;

        public SceneController scenecontroller;
        public string sceneName;
        public TitleButton titlebutton;
        public ButtonManager buttonmanager;

        bool isEscape;

        void Start()
        {
            isGameClear = false;
            isGameOver = false;
            isGameResume = false;
            flag = true;
            isEscape = true;
        }

        void Update()
        {
            if (Input.GetKey(KeyCode.Escape) && isEscape)
            {
                titlebutton.OnGameStart();
                buttonmanager.OnButton();
                scenecontroller.sceneChange(sceneName);
                isEscape = false;
            }

            if (EnemyGage.currentHp <= 0)
            {
                isGameClear = true;
            }

            if (TimeGage.currentTime <= 0)
            {
                isGameOver = true;
            }

            if (isGameClear)
            {
                isGameOver = false;
                //Time.timeScale = 0f;
                audioSource.volume = 0f;
                audioSource2.volume = 0f;
                Debug.Log("isGameResume FFF");
                playUI.SetActive(false);
                GameClearUI.SetActive(true);
            }

            if (isGameOver)
            {
                isGameClear = false;
                //Time.timeScale = 0f;
                audioSource.volume = 0f;
                audioSource2.volume = 0f;
                Debug.Log("isGameResume FFF");
                //scenecontroller.sceneChange(sceneName);
                playUI.SetActive(false);
                resultUI.SetActive(true);
                //flag = false;
            }

            if (isGameResume)
            {
               // Time.timeScale = 1f;
                //audioSource.volume = 1f;
                //audioSource2.volume = 1f;
                Debug.Log("isGameResume TTT");
                isGameResume = false;
            }
        }
    }
}
