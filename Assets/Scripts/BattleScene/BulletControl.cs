using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    [SerializeField]
    private float speed;

    void Start()
    {
        transform.rotation = Quaternion.Euler(0f,0f, 180f);
    }

    void Update()
    {
        transform.position += new Vector3(0, -speed * Time.deltaTime, 0);

        if (transform.position.y > 300) Destroy(gameObject);
        if (transform.position.x < -300 || transform.position.x > 300) Destroy(gameObject);
        if (transform.position.z < -300 || transform.position.z > 300) Destroy(gameObject);
    }
}
