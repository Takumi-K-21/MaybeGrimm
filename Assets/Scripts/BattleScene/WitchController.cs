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

    [SerializeField] private float v_x; // x•ûŒü‚Ì‘¬“x
    [SerializeField] private float v_y; // y•ûŒü‚Ì‘¬“x
    [SerializeField] private float v_z; // z•ûŒü‚Ì‘¬“x
    [SerializeField] private int n; // n-way’e
    [SerializeField] private float theta; // ”­ËŠp“x

    [SerializeField] private float vec1_x;
    [SerializeField] private float vec1_y;
    [SerializeField] private float vec1_z;

    [SerializeField] private float vec2_x;
    [SerializeField] private float vec2_y;
    [SerializeField] private float vec2_z;

    private int rand;
    private float gameTime; // Œo‰ßŠÔ
    private float intervalTime; // UŒ‚ŠÔŠu
    private GameObject player;

    private Vector3 vec1; // ”j‰ó‰Â”\‚È’e‚Ì”­ËˆÊ’u
    private Vector3 vec2; // ”j‰ó•s‰Â”\‚È’e‚Ì”­ËˆÊ’u

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
    private int n2; // n-way’e
    [SerializeField]
    private float theta2; // ”­ËŠp“x

    // •Ï”‚Ì‰Šú‰»
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rand = Random.Range(0, 3); // 0‚©‚ç3‚Ì”ÍˆÍ‚Å—”‚ğ¶¬
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

        gameTime += Time.deltaTime; // Œo‰ßŠÔ‚ÌXV

        if (intervalTime <= gameTime)
        {
            Attack(); // UŒ‚ƒpƒ^[ƒ“ŠÖ”‚ÌŒÄ‚Ño‚µ
            rand = Random.Range(0, 3); // 0‚©‚ç3‚Ì”ÍˆÍ‚Å—”‚ğ¶¬
            gameTime = 0f; // Œo‰ßŠÔ‚Ì‰Šú‰»
        }
    }

    // UŒ‚ƒpƒ^[ƒ“ŠÖ”
    void Attack()
    {
        if (rand == 0 && isBullet)
        { // ”j‰ó‰Â”\‚È’e‚ğ”­Ë
            isBullet = false;
            StartCoroutine("WindAttack");
            intervalTime = Random.Range(0f, 3f); // 0‚©‚ç3‚Ì”ÍˆÍ‚Å—”‚ğ¶¬
        }
        else if (rand == 1 && isBullet2)
        { // ”j‰ó•s‰Â”\‚È’e‚ğ”­Ë
            isBullet2 = false;
            StartCoroutine("renshaAttack");

            intervalTime = Random.Range(0f, 3f); // 0‚©‚ç3‚Ì”ÍˆÍ‚Å—”‚ğ¶¬
        }
        else if (rand == 2 && !jumpNow)
        { // ƒWƒƒƒ“ƒvƒAƒNƒVƒ‡ƒ“
            rb.velocity = Vector3.zero;
            rb.AddForce(0f, 750f, 0f);
            jumpNow = true;
            intervalTime = Random.Range(0f, 3f); // 0‚©‚ç3‚Ì”ÍˆÍ‚Å—”‚ğ¶¬
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
        //  float rad = Mathf.PI / 180 * 10; // ’e‚ÌÅ‰‚ÌŠp“x
        float rad = Mathf.PI / 180 * 90; // ’e‚ÌÅ‰‚ÌŠp“x
        float rad_step = Mathf.PI / 180 * theta2 / (n2 - 1); // ’e‚Æ’e‚ÌŠÔ‚ÌŠp“x

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
                float r = Random.Range(0, 2); // 0‚©‚ç1‚Ì”ÍˆÍ‚Å—”‚ğ¶¬
                if (r == 0)
                {
                    Vector3 vec = transform.position;
                    vec.y = 2.5f;
                    jumpNow = false;

                    float rad = Mathf.PI / 180 * 10; // ’e‚ÌÅ‰‚ÌŠp“x
                    float rad_step = Mathf.PI / 180 * theta / (n - 1); // ’e‚Æ’e‚ÌŠÔ‚ÌŠp“x

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

                    float rad = Mathf.PI / 180 * 10; // ’e‚ÌÅ‰‚ÌŠp“x
                    float rad_step = Mathf.PI / 180 * theta / (n - 1); // ’e‚Æ’e‚ÌŠÔ‚ÌŠp“x

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
