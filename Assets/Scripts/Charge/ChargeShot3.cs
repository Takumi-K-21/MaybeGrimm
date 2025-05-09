using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeShot3 : MonoBehaviour
{
    float velocity;
    [SerializeField] GameObject target;
    // private float speed = 10f;
    Vector3 direction;
    public GameObject EnemyPos;

    private float vec_x, vec_y, vec_z;

    private GameObject enemy;

    [SerializeField] private Vector3 _forward = Vector3.forward;

    void Start()
    {
        enemy = GameObject.Find("EnemyPos");
        // transform.LookAt(player.transform);
        var dir = enemy.transform.position - transform.position;
        var lookAtRotation = Quaternion.LookRotation(dir, Vector3.up);
        var offsetRotation = Quaternion.FromToRotation(_forward, Vector3.forward);
        // offsetRotation += Quaternion.Euler(0f, 90f, 0f);
        transform.rotation = lookAtRotation * offsetRotation * Quaternion.Euler(0f, 0f, 45f);
        //transform.rotation = offsetRotation * Quaternion.Euler(0f, 0f, 0f);
    }

    public void setVelocity(float v_x, float v_y, float v_z)
    {
        vec_x = v_x;
        vec_y = v_y;
        vec_z = v_z;
    }

    void Update()
    {
        // transform.position += vec * Time.deltaTime;

        // transformを取得
        Transform myTransform = this.transform;

        // 座標を取得
        Vector3 pos = myTransform.position;
        pos.x += vec_x * Time.deltaTime;    // x座標へ0.01加算
        pos.y += vec_y * Time.deltaTime;
        pos.z += vec_z * Time.deltaTime;    // z座標へ0.01加算

        myTransform.position = pos;  // 座標を設定

        if (transform.position.x < -300 || transform.position.x > 300) Destroy(gameObject);
        if (transform.position.z < -300 || transform.position.z > 300) Destroy(gameObject);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
            EnemyGage.currentHp -= 1f + (PlayerController3.lv-40);
        }
    }
}
