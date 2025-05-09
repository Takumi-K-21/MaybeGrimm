using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleLeft : MonoBehaviour
{
    public GameObject particle;

    public static bool windLeft;

    void Start()
    {
        windLeft = false;
    }

    void Update()
    {
        if (windLeft)
        {
            Instantiate(particle, transform.position, Quaternion.Euler(0, 90, 0));
            windLeft = false;
        }
    }
}
