using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchController : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject bullet_01;
    public GameObject bullet_02;
    public GameObject bullet_03;

    private bool jumpNow = false;

    [SerializeField] private float v_x; // x方向の速度
    [SerializeField] private float v_y; // y方向の速度
    [SerializeField] private float v_z; // z方向の速度
    [SerializeField] private int n; // n-way弾
    [SerializeField] private float theta; // 発射角度

    [SerializeField] private float vec1_x;
    [SerializeField] private float vec1_y;
    [SerializeField] private float vec1_z;

    [SerializeField] private float vec2_x;
    [SerializeField] private float vec2_y;
    [SerializeField] private float vec2_z;

    private int rand;
    private float gameTime; // 経過時間
    private float intervalTime; // 攻撃間隔
    private GameObject player;

    private Vector3 vec1; // 破壊可能な弾の発射位置
    private Vector3 vec2; // 破壊不可能な弾の発射位置

    public AudioClip ImpluseSound;
    AudioSource audioSource;

    [SerializeField]
    private GameObject markerPrefab;

    [SerializeField]
    private GameObject bulletPrefab;

    private GameObject marker;

    private Vector3 launcher;

    public static bool isBullet;
    public static bool isBullet2;

    private float attack_time;

    public GameObject prefab;
    public GameObject prefab2;

    [SerializeField]
    private int n2; // n-way弾
    [SerializeField]
    private float theta2; // 発射角度

    // 変数の初期化
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rand = Random.Range(0, 3); // 0から3の範囲で乱数を生成
        gameTime = 0f;
        intervalTime = 3f;
        player = GameObject.FindGameObjectWithTag("target");
        vec1 = transform.position;
        vec1.x += vec1_x;
        vec1.y += vec1_y;
        vec1.z += vec1_z;
        vec2 = transform.position;
        vec2.x += vec2_x;
        vec2.y += vec2_y;
        vec2.z += vec2_z;

        audioSource = GetComponent<AudioSource>();

        isBullet = true;
        isBullet2 = true;

        attack_time = 1f;
    }

    void Update()
    {
        if (BattleScene.GameManager.isGameClear || BattleScene.GameManager.isGameOver)
        {
            return;
        }

        gameTime += Time.deltaTime; // 経過時間の更新

        if (intervalTime <= gameTime)
        {
            Attack(); // 攻撃パターン関数の呼び出し
            rand = Random.Range(0, 3); // 0から3の範囲で乱数を生成
            gameTime = 0f; // 経過時間の初期化
        }
    }

    // 攻撃パターン関数
    void Attack()
    {
        if (rand == 0 && isBullet)
        { // 破壊可能な弾を発射
            isBullet = false;
            StartCoroutine("WindAttack");
            intervalTime = Random.Range(0f, 3f); // 0から3の範囲で乱数を生成
        }
        else if (rand == 1 && isBullet2)
        { // 破壊不可能な弾を発射
            isBullet2 = false;
            StartCoroutine("renshaAttack");

            intervalTime = Random.Range(0f, 3f); // 0から3の範囲で乱数を生成
        }
        else if (rand == 2 && !jumpNow)
        { // ジャンプアクション
            rb.velocity = Vector3.zero;
            rb.AddForce(0f, 750f, 0f);
            jumpNow = true;
            intervalTime = Random.Range(0f, 3f); // 0から3の範囲で乱数を生成
        }
    }

    IEnumerator renshaAttack()
    {
        for (int i = 0; i < 30; i++)
        {
            Instantiate(prefab2, vec2, Quaternion.identity);
            yield return new WaitForSeconds(0.25f);
        }

        isBullet2 = true;
    }

    IEnumerator WindAttack()
    {
        //  float rad = Mathf.PI / 180 * 10; // 弾の最初の角度
        float rad = Mathf.PI / 180 * 90; // 弾の最初の角度
        float rad_step = Mathf.PI / 180 * theta2 / (n2 - 1); // 弾と弾の間の角度

        Vector3 v3 = new Vector3(vec1.x, vec1.y, vec1.z);

        for (int i = 0; i < n2; i++)
        {
            v3 += new Vector3(3 * Mathf.Cos(rad), 0f, -3 * Mathf.Sin(rad));

            GameObject bullet = Instantiate(prefab, v3, Quaternion.identity);

            BulletController2 bulletScript = bullet.GetComponent<BulletController2>();

            yield return new WaitForSeconds(0.25f);

            bulletScript.SetBullet(Mathf.Cos(rad), 0.0f, -Mathf.Sin(rad));

            rad += rad_step;
        }

        isBullet = true;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player Bullet")
        {
        }

        if (jumpNow == true)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                audioSource.PlayOneShot(ImpluseSound);
                float r = Random.Range(0, 2); // 0から1の範囲で乱数を生成
                if (r == 0)
                {
                    Vector3 vec = transform.position;
                    vec.y = 2.5f;
                    jumpNow = false;

                    float rad = Mathf.PI / 180 * 10; // 弾の最初の角度
                    float rad_step = Mathf.PI / 180 * theta / (n - 1); // 弾と弾の間の角度

                    for (int i = 0; i < n; i++)
                    {
                        GameObject bullet = Instantiate(bullet_01, vec, transform.rotation) as GameObject;

                        EnemyBullet bulletScript = bullet.GetComponent<EnemyBullet>();

                        bulletScript.setVelocity(50 * Mathf.Cos(rad), 0, -50 * Mathf.Sin(rad));

                        rad += rad_step;
                    }
                }
                else if (r == 1)
                {
                    Vector3 vec = transform.position;
                    vec.y = 1.5f;
                    jumpNow = false;

                    float rad = Mathf.PI / 180 * 10; // 弾の最初の角度
                    float rad_step = Mathf.PI / 180 * theta / (n - 1); // 弾と弾の間の角度

                    for (int i = 0; i < n; i += 3)
                    {
                        vec.y = 1.5f;

                        for (int j = 0; j < 5; j++)
                        {
                            GameObject bullet = Instantiate(bullet_02, vec, transform.rotation) as GameObject;

                            EnemyBullet2 bulletScript = bullet.GetComponent<EnemyBullet2>();

                            bulletScript.setVelocity(50 * Mathf.Cos(rad), 0, -50 * Mathf.Sin(rad));

                            vec.y += 3;
                        }

                        rad += rad_step * 3;
                    }
                }
            }
        }
    }
}
