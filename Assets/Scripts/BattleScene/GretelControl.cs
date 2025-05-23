using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GretelControl : MonoBehaviour
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

        isBullet = false;
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
        if (rand == 0)
        { // 破壊可能な弾を発射
            float direction_x = player.transform.position.x - vec1.x;
            float direction_y = player.transform.position.y - vec1.y;
            float direction_z = player.transform.position.z - vec1.z;

            GameObject bullet = Instantiate(bullet_03, vec1, transform.rotation) as GameObject;
            BulletSplit bulletScript = bullet.GetComponent<BulletSplit>();
            bulletScript.setVelocity(v_x * direction_x, v_y * direction_y, v_z * direction_z);

            intervalTime = Random.Range(0f, 3f); // 0から3の範囲で乱数を生成
        }
        else if (rand == 1 && !isBullet && !GroundControl.isDelete)
        { // 破壊不可能な弾を発射
            /*
            float direction_x = player.transform.position.x - vec2.x;
            float direction_y = player.transform.position.y - vec2.y;
            float direction_z = player.transform.position.z - vec2.z;

            GameObject bullet = Instantiate(bullet_02, vec2, transform.rotation) as GameObject;
            EnemyBullet2 bulletScript = bullet.GetComponent<EnemyBullet2>();
            bulletScript.setVelocity(v_x * direction_x, v_y * direction_y, v_z * direction_z);

            Debug.Log("bullet!");
            */

            GroundControl.isDelete = true;

            launcher = player.transform.position;

            Invoke("Shot", 1.0f);

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

    void Shot()
    {
        StartCoroutine("rensha");
    }

    IEnumerator rensha()
    {
        for (int i = 0; i < 10; i++)
        {
            Instantiate(bulletPrefab, vec2, Quaternion.identity);

            yield return new WaitForSeconds(0.05f);
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
