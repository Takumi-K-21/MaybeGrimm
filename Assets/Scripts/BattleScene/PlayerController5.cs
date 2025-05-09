using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController5 : MonoBehaviour {
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

    private float power;
    private float game_time;

    private bool wind_bool;
    private float wind_time;

    private bool set_rand;
    private int rand;
    
    public GameObject leftArrow;
    public GameObject rightArrow;

    bool isSoundDamage;
    float SoundDamege = 0f;

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
        lv = 81;
        exp = 0;

        Physics.gravity = new Vector3(0, -50f, 0);

        audioSource = GetComponent<AudioSource>();

        isSlow = false;
        slow_time = 0.0f;

        jumpPower = 30f;

        power = 2.5f;
        game_time = 0f;
        wind_bool = false;
        wind_time = 0f;

        set_rand = false;

        isSoundDamage = false;

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

        SoundDamege += Time.deltaTime;

        if (SoundDamege >= 0.5f)
        {
            isSoundDamage = true;
        }

        game_time += Time.deltaTime;

        // if (game_time >= 3f) wind_bool = true;

        if (wind_bool)
        {
            Debug.Log("wind_bool");

            if (!set_rand)
            {
                rand = Random.Range(0, 2);
                set_rand = true;
            }

            wind_time += Time.deltaTime;

            if (rand == 0) leftArrow.SetActive(true);
            if (rand == 1) rightArrow.SetActive(true);

            if (rand == 0 && sumAngle <= 60f)
            {
                transform.RotateAround(target.transform.position, Vector3.up, 7.5f * Time.deltaTime);
                sumAngle += 7.5f * Time.deltaTime;
            }
            if (rand == 1 && sumAngle >= -60f)
            {
                transform.RotateAround(target.transform.position, Vector3.up, -7.5f * Time.deltaTime);
                sumAngle -= 7.5f * Time.deltaTime;
            }

            if (wind_time >= 3f)
            {
                game_time = 0f;
                wind_bool = false;
                wind_time = 0f;
                set_rand = false;

                leftArrow.SetActive(false);
                rightArrow.SetActive(false);
            }
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
                PlayerBullet5 bulletScript = bullet.GetComponent<PlayerBullet5>();
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
            ChargeShot5 bulletScript = cBullet.GetComponent<ChargeShot5>();
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
            bool flag = LevelSystem5.EndBattle();
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

            SetImage.isCream = true;
            SetImage.isFlag = true;

            slow_time = 0.0f;
            angle = 5.0f;
            jumpPower = 20f;
            isSlow = true;
        }

        if (other.gameObject.tag == "ChocoBullet")
        {
            TimeGage.currentTime -= 3f;
            var source = GetComponent<Cinemachine.CinemachineImpulseSource>();
            source.GenerateImpulse();

            if (isSoundDamage)
            {
                audioSource.PlayOneShot(damageSound);
                isSoundDamage = false;
            }

            SetImage.damageFlag = true;
        }
    }
}
