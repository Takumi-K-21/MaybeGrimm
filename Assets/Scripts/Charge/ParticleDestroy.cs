using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroy : MonoBehaviour
{
    ParticleSystem particle;

    public static bool isFlag;

    void Start()
    {
        particle = GetComponent<ParticleSystem>();
        isFlag = false;
    }

    void Update()
    {
        if (isFlag)
        {
            //Destroy(gameObject);
            isFlag = false;
        }
    }
}
