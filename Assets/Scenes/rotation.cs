using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotation : MonoBehaviour
{
    float x, y, z;
    // Start is called before the first frame update
    void Start()
    {
       x = y = z = 0;
    }

    void Update()
    {
        gameObject.transform.rotation = Quaternion.Euler(x, y, z);
    }
}
