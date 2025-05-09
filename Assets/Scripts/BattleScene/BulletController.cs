using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    private float speed;
    private float gravity = -80f;
    private float velocity;
    private float time;

    void Start()
    {
        time = 0.0f;
    }

    void Update()
    {
        time += Time.deltaTime;
        velocity = -speed + gravity * time;
        transform.position += new Vector3(0, velocity * Time.deltaTime, 0);

        if (transform.position.y < -300) Destroy(gameObject);
        if (transform.position.x < -300 || transform.position.x > 300) Destroy(gameObject);
        if (transform.position.z < -300 || transform.position.z > 300) Destroy(gameObject);
    }
}
