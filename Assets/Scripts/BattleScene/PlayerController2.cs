using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController2 : MonoBehaviour {
    [SerializeField] GameObject target; // EnemyのGameObject
    private Rigidbody rb; // PlayerのRigidbody
    private float angle; // 回転の角度
    private float interval;
    private float sumAngle;

    [SerializeField] GameObject sphere;
    [SerializeField] GameObject childObj;
    [SerializeField] GameObject childObj2;

    public static int lv;
    public static int exp;

    private bool isJump;
    private float jumpPower;

    public AudioClip damageSound;
    AudioSource audioSource;

    [SerializeField] private float v_x; // x方向の速度
    [SerializeField] private float v_y; // y方向の速度
    [SerializeField] private float v_z; // z方向の速度

    bool isSlow;
    float slow_time;

    public static float chargeMax;
    public GameObject chargePrefab;
    public static bool isCharge;

    public AudioClip ChargeSound;
    public AudioClip ShotSound;

    private Animator animator;

    private const string key_isRun = "isRun";

    void Start() {
        // 変数の初期化
        rb = GetComponent<Rigidbody>();
        angle = 15f;
        interval = 0f;

        childObj = transform.GetChild(5).gameObject;
        childObj2 = transform.GetChild(6).gameObject;
        sumAngle = 0f;
        lv = 21;
        exp = 0;

        Physics.gravity = new Vector3(0, -50f, 0);

        audioSource = GetComponent<AudioSource>();

        isSlow = false;
        slow_time = 0.0f;

        jumpPower = 30f;

        isCharge = false;
        chargeMax = 0f;

        // 自分に設定されているAnimatorコンポーネントを習得する
        this.animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (BattleScene.GameManager.isGameClear || BattleScene.GameManager.isGameOver)
        {
            return;
        }

        var col = GetComponent<CapsuleCollider>();

        if (col.bounds.size.x <= 0) transform.position = new Vector3(transform.position.x, 0, transform.position.z);

        interval += Time.deltaTime;

        if (isSlow)
        {
            slow_time += Time.deltaTime;

            if(slow_time > 7.5f)
            {
                isSlow = false;
                slow_time = 0.0f;
                angle = 15.0f;
                jumpPower = 30f;
            }
        }

        // 弾の発射
        if (interval >= 0.5f)
        {
            float direction_x = childObj.transform.position.x - childObj2.transform.position.x;
            float direction_y = childObj.transform.position.y - childObj2.transform.position.y;
            float direction_z = childObj.transform.position.z - childObj2.transform.position.z;

            if (Input.GetKey(KeyCode.LeftControl))
            {
                GameObject bullet = Instantiate(sphere, childObj.transform.position, transform.rotation) as GameObject;
                PlayerBullet2 bulletScript = bullet.GetComponent<PlayerBullet2>();
                bulletScript.setVelocity(v_x * direction_x, v_y * direction_y, v_z * direction_z);
                interval = 0f;
            }
        }

        // 左右移動
        if (Input.GetKey(KeyCode.LeftArrow) && sumAngle <= 60f) {
            transform.RotateAround(target.transform.position, Vector3.up, angle * Time.deltaTime);
            sumAngle += angle * Time.deltaTime;
            this.animator.SetBool(key_isRun, true);
        } else if (Input.GetKey(KeyCode.RightArrow) && sumAngle >= -60f) {
            transform.RotateAround(target.transform.position, Vector3.up, -angle * Time.deltaTime);
            sumAngle -= angle * Time.deltaTime;
            this.animator.SetBool(key_isRun, true);
        }
        else
        {
            this.animator.SetBool(key_isRun, false);
        }

        if (Input.GetKey(KeyCode.DownArrow)) rb.velocity -= Vector3.up * 1f;

        // ジャンプアクション
        if (Input.GetKeyDown(KeyCode.UpArrow) && !isJump) {
            isJump = true;
            rb.velocity += Vector3.up * jumpPower; 
        }

        if (chargeMax >= 10 && !isCharge)
        {
            Debug.Log("CHARGEMAX!");
            isCharge = true;
            audioSource.PlayOneShot(ChargeSound);
        }
        else
        {
            chargeMax += Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.LeftShift) && isCharge)
        {
            float direction_x = childObj.transform.position.x - childObj2.transform.position.x;
            float direction_y = childObj.transform.position.y - childObj2.transform.position.y;
            float direction_z = childObj.transform.position.z - childObj2.transform.position.z;

            GameObject cBullet = Instantiate(chargePrefab, childObj.transform.position, transform.rotation) as GameObject;
            ChargeShot2 bulletScript = cBullet.GetComponent<ChargeShot2>();
            bulletScript.setVelocity(v_x * direction_x, v_y * direction_y, v_z * direction_z);

            audioSource.PlayOneShot(ShotSound);

            chargeMax = 0f;
            isCharge = false;
        }
    }

    // 地面との接地判定を調べる
    void OnCollisionEnter(Collision other) {
        if (BattleScene.GameManager.isGameClear || BattleScene.GameManager.isGameOver)
        {
            return;
        }

        if (other.gameObject.name == "Ground") {
            isJump = false;
        }

        if (other.gameObject.tag == "Exp") {
            bool flag = LevelSystem2.EndBattle();
            SetImage.isGet = true;
        }

        if (other.gameObject.tag == "Enemy Bullet")
        {
            TimeGage.currentTime -= 10f;
            var source = GetComponent<Cinemachine.CinemachineImpulseSource>();
            source.GenerateImpulse();

            audioSource.PlayOneShot(damageSound);

            SetImage.damageFlag = true;
        }

        if (other.gameObject.tag == "Enemy Bullet 2")
        {
            TimeGage.currentTime -= 10f;
            var source = GetComponent<Cinemachine.CinemachineImpulseSource>();
            source.GenerateImpulse();

            audioSource.PlayOneShot(damageSound);

            SetImage.damageFlag = true;
        }

        if (other.gameObject.tag == "Enemy Bullet 3")
        {
            TimeGage.currentTime -= 10f;
            var source = GetComponent<Cinemachine.CinemachineImpulseSource>();
            source.GenerateImpulse();

            audioSource.PlayOneShot(damageSound);

            SetImage.damageFlag = true;

            slow_time = 0.0f;
            angle = 5.0f;
            jumpPower = 20f;
            isSlow = true;
        }
    }
}
