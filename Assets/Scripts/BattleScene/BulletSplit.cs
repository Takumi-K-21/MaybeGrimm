using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSplit : MonoBehaviour
{
    [SerializeField] private int n; // n-way弾
    [SerializeField] private float theta; // 発射角度

    public GameObject bullet_01;

    public GameObject Exp;
    public GameObject E;

    private float vec_x, vec_y, vec_z;

    private float time;

    private GameObject player;

    private float rand;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("target");
        time = 0.0f;
    }

    public void setVelocity(float v_x, float v_y, float v_z)
    {
        vec_x = v_x / 5;
        vec_y = v_y / 5;
        vec_z = v_z / 5;
    }

    void Update()
    {
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

        if (time > 1.0f)
        {
            Vector3 vec = transform.position;
            Vector3 direction = (player.transform.position - transform.position).normalized;

            float rad = Mathf.PI / 180 * 10; // 弾の最初の角度
            float rad_step = Mathf.PI / 180 * theta / (n - 1); // 弾と弾の間の角度

            for (int i = 0; i < n; i++)
            {
                GameObject bullet = Instantiate(bullet_01, vec, transform.rotation) as GameObject;

                CreamBullet bulletScript = bullet.GetComponent<CreamBullet>();

                bulletScript.setVelocity(30 * Mathf.Cos(rad), 30 * direction.y, -30 * Mathf.Sin(rad));

                rad += rad_step;
            }

            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
