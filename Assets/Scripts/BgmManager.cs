using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmManager : MonoBehaviour
{
    // private static bool isLoad = false;// 自身がすでにロードされているかを判定するフラグ

    public AudioSource audioSourceBGM;
    public AudioClip bgm;

    public void DestroyBgm()
    {
       Destroy(gameObject);
        Debug.Log("BGM!");
    }

    public static BgmManager Instance
    {
        get; private set;
    }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        audioSourceBGM = this.GetComponent<AudioSource>();
    }
}