using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnController : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject bullet_01;
    public GameObject bullet_02;
    public GameObject bullet_03;
    public GameObject bullet_04;
    public GameObject bullet_05;

    public GameObject poizon;

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

    [SerializeField] private float vec3_x;
    [SerializeField] private float vec3_y;
    [SerializeField] private float vec3_z;

    [SerializeField] private float vec4_x;
    [SerializeField] private float vec4_y;
    [SerializeField] private float vec4_z;

    [SerializeField] private float vec5_x;
    [SerializeField] private float vec5_y;
    [SerializeField] private float vec5_z;

    private int rand;
    private float gameTime; // Œo‰ßŠÔ
    private float intervalTime; // UŒ‚ŠÔŠu
    private GameObject player;

    private Vector3 vec1; // ‰HªUŒ‚
    private Vector3 vec2; // ”j‰ó•s‰Â”\‚È’e‚Ì”­ËˆÊ’u
    private Vector3 vec3; // ¶ƒNƒŠ[ƒ€UŒ‚
    private Vector3 vec4; // ƒŠƒ“ƒSUŒ‚
    private Vector3 vec5; // ƒŒ[ƒU[UŒ‚

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
    public static bool isRensha;

    private float attack_time;

    public GameObject prefab;
    public GameObject prefab2;

    [SerializeField]
    private int n2; // n-way’e
    [SerializeField]
    private float theta2; // ”­ËŠp“x

    public static bool isChoco;

    // •Ï”‚Ì‰Šú‰»
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rand = Random.Range(0, 7); // 0‚©‚ç4‚Ì”ÍˆÍ‚Å—”‚ğ¶¬
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
        vec3 = transform.position;
        vec3.x += vec3_x;
        vec3.y += vec3_y;
        vec3.z += vec3_z;
        vec4 = transform.position;
        vec4.x += vec4_x;
        vec4.y += vec4_y;
        vec4.z += vec4_z;
        vec5 = transform.position;
        vec5.x += vec5_x;
        vec5.y += vec5_y;
        vec5.z += vec5_z;

        audioSource = GetComponent<AudioSource>();

        isBullet = true;
        isBullet2 = true;

        attack_time = 1f;

        isRensha = true;
        isChoco = false;
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
            rand = Random.Range(0, 7); // 0‚©‚ç3‚Ì”ÍˆÍ‚Å—”‚ğ¶¬
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
        else if (rand == 3)
        { // ”j‰ó‰Â”\‚È’e‚ğ”­Ë
            float direction_x = player.transform.position.x - vec3.x;
            float direction_y = player.transform.position.y - vec3.y;
            float direction_z = player.transform.position.z - vec3.z;

            GameObject bullet = Instantiate(bullet_03, vec3, transform.rotation) as GameObject;
            BulletSplit bulletScript = bullet.GetComponent<BulletSplit>();
            bulletScript.setVelocity(v_x * direction_x, v_y * direction_y, v_z * direction_z);

            intervalTime = Random.Range(0f, 3f); // 0‚©‚ç3‚Ì”ÍˆÍ‚Å—”‚ğ¶¬
        }
        else if (rand == 4)
        { // ”j‰ó•s‰Â”\‚È’e‚ğ”­Ë

            GameObject bullet = Instantiate(bullet_04, vec4, transform.rotation) as GameObject;

            intervalTime = Random.Range(0f, 1f); // 1‚©‚ç3‚Ì”ÍˆÍ‚Å—”‚ğ¶¬

            Debug.Log("bullet!");
        }
        else if (rand == 5 && isRensha)
        {
            isRensha = false;

            StartCoroutine("rensha");

            intervalTime = Random.Range(0f, 3f); // 1‚©‚ç3‚Ì”ÍˆÍ‚Å—”‚ğ¶¬

            Debug.Log("bullet!");
        }
        else if (rand == 6 && !isChoco && !GroundControl.isDelete)
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

    IEnumerator rensha()
    {
        for (int i = 0; i < 30; i++)
        {
            // ”j‰ó‰Â”\‚È’e‚ğ”­Ë
            float direction_x = player.transform.position.x - vec5.x;
            float direction_y = player.transform.position.y - vec5.y;
            float direction_z = player.transform.position.z - vec5.z;

            GameObject bullet = Instantiate(poizon, vec5, transform.rotation) as GameObject;
            EnemyBullet1 bulletScript = bullet.GetComponent<EnemyBullet1>();
            bulletScript.setVelocity(1.5f * v_x * direction_x, 1.5f * v_y * direction_y, 1.5f * v_z * direction_z);

            yield return new WaitForSeconds(0.05f);
        }

        isRensha = true;
    }

    void Shot()
    {
        StartCoroutine("groundRensha");
    }

    IEnumerator groundRensha()
    {
        for (int i = 0; i < 10; i++)
        {
            Instantiate(bulletPrefab, vec2, Quaternion.identity);

            yield return new WaitForSeconds(0.05f);
        }

        isChoco = true;
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
                    vec.y = 1.5f;
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
