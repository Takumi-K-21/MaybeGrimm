using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleRight : MonoBehaviour
{
    public GameObject particle;

    public static bool windRight;

    void Start()
    {
        windRight = false;
    }

    void Update()
    {
        if (windRight)
        {
            Instantiate(particle, transform.position, Quaternion.Euler(0, -90, 0));
            windRight = false;
        }
    }
}
