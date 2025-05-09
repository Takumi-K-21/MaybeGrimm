using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private float speed;

    private GameObject came;

    private Vector3 dir;

    private float time;

    private Vector3 angle;

    private Vector3 angle_rnd;

    private float a;

    private float rand;

    public GameObject Exp;

    private GameObject player;

    [SerializeField] private Vector3 _forward = Vector3.forward;

    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("target");
        // transform.LookAt(player.transform);
        var dir_a = player.transform.position - transform.position;
        var lookAtRotation = Quaternion.LookRotation(dir_a, Vector3.up);
        var offsetRotation = Quaternion.FromToRotation(_forward, Vector3.forward);
        // offsetRotation += Quaternion.Euler(0f, 90f, 0f);
        transform.rotation = lookAtRotation * offsetRotation * Quaternion.Euler(0f, -90f, 180f);


        came = GameObject.FindGameObjectWithTag("MainCamera");
        dir = came.transform.position - transform.position;
        time = 0f;
        angle = new Vector3(30f, 30f, 30f);
        speed = Random.Range(0.25f, 0.75f);
        angle_rnd.x = Random.Range(-0.5f, 0.5f);
        angle_rnd.y = Random.Range(-0.5f, 0.5f);
        angle_rnd.z = Random.Range(-0.5f, 0.5f);

        angle.x *= angle_rnd.x;
        angle.y *= angle_rnd.y;
        angle.z *= angle_rnd.z;

        dir = dir + angle;
    }

    // Update is called once per frame
    void Update()
    {
        // transformÇéÊìæ
        Transform myTransform = this.transform;

        // ç¿ïWÇéÊìæ
        Vector3 pos = myTransform.position;
        pos.x += speed * dir.x * Time.deltaTime;
        pos.y += speed * dir.y * Time.deltaTime;
        pos.z += speed * dir.z * Time.deltaTime;

        myTransform.position = pos;  // ç¿ïWÇê›íË

        if (transform.position.y < -300) Destroy(gameObject);
        if (transform.position.x < -300 || transform.position.x > 300) Destroy(gameObject);
        if (transform.position.z < -300 || transform.position.z > 300) Destroy(gameObject);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "Player Bullet" || other.gameObject.tag == "CS")
        {
            Destroy(gameObject);

            rand = Random.Range(0f, 1f);

            if (rand >= 0.5f)
            {
                GameObject exp = Instantiate(Exp, transform.position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;

                EController expScript = exp.GetComponent<EController>();

                expScript.setVelocity(dir.x, dir.y, dir.z);
            }
        }
    }
}
