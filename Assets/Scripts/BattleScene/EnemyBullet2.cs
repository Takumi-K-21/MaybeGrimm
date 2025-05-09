using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet2 : MonoBehaviour
{
    private int n;
    public GameObject Exp;
    public GameObject E;

    private float vec_x, vec_y, vec_z;

    private static float time;

    private float rand;

    void Start()
    {
        time = 0f;
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

        time += Time.deltaTime;

        // transformを取得
        Transform myTransform = this.transform;

        // 座標を取得
        Vector3 pos = myTransform.position;
        pos.x += vec_x * Time.deltaTime;    // x座標へ0.01加算
        pos.y += vec_y * Time.deltaTime;
        pos.z += vec_z * Time.deltaTime;    // z座標へ0.01加算

        myTransform.position = pos;  // 座標を設定

        if (transform.position.y < -300) Destroy(gameObject);
        if (transform.position.x < -300 || transform.position.x > 300) Destroy(gameObject);
        if (transform.position.z < -300 || transform.position.z > 300) Destroy(gameObject);

    }

    void OnCollisionEnter(Collision other)
    {
        if (time <= 0.25f) return;

        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "CS")
        {
            Destroy(gameObject);

            rand = Random.Range(0f, 1f);

            if (rand >= 0.5f)
            {
                GameObject exp = Instantiate(Exp, transform.position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;

                EController expScript = exp.GetComponent<EController>();

                expScript.setVelocity(vec_x, vec_y, vec_z);
            }
        }
    }
}
