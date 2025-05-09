using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSplit : MonoBehaviour
{
    [SerializeField] private int n; // n-way�e
    [SerializeField] private float theta; // ���ˊp�x

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

        // transform���擾
        Transform myTransform = this.transform;

        // ���W���擾
        Vector3 pos = myTransform.position;
        pos.x += vec_x * Time.deltaTime;    // x���W��0.01���Z
        pos.y += vec_y * Time.deltaTime;
        pos.z += vec_z * Time.deltaTime;    // z���W��0.01���Z

        myTransform.position = pos;  // ���W��ݒ�

        if (transform.position.y < -300) Destroy(gameObject);
        if (transform.position.x < -300 || transform.position.x > 300) Destroy(gameObject);
        if (transform.position.z < -300 || transform.position.z > 300) Destroy(gameObject);

        if (time > 1.0f)
        {
            Vector3 vec = transform.position;
            Vector3 direction = (player.transform.position - transform.position).normalized;

            float rad = Mathf.PI / 180 * 10; // �e�̍ŏ��̊p�x
            float rad_step = Mathf.PI / 180 * theta / (n - 1); // �e�ƒe�̊Ԃ̊p�x

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
