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

        isBullet = false;
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
        if (rand == 0)
        { // ”j‰ó‰Â”\‚È’e‚ğ”­Ë
            float direction_x = player.transform.position.x - vec1.x;
            float direction_y = player.transform.position.y - vec1.y;
            float direction_z = player.transform.position.z - vec1.z;

            GameObject bullet = Instantiate(bullet_03, vec1, transform.rotation) as GameObject;
            BulletSplit bulletScript = bullet.GetComponent<BulletSplit>();
            bulletScript.setVelocity(v_x * direction_x, v_y * direction_y, v_z * direction_z);

            intervalTime = Random.Range(0f, 3f); // 0‚©‚ç3‚Ì”ÍˆÍ‚Å—”‚ğ¶¬
        }
        else if (rand == 1 && !isBullet && !GroundControl.isDelete)
        { // ”j‰ó•s‰Â”\‚È’e‚ğ”­Ë
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
