using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeManager : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource audioSourceSE;
    public AudioClip se;

    public static SeManager Instance
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
        audioSourceSE = this.GetComponent<AudioSource>();
    }

    public void SeLoad()
    {
        audioSourceSE.PlayOneShot(se);
        Debug.Log("SE!");
    }
}