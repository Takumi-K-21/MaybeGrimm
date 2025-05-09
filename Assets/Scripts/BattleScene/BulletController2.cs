using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController2 : MonoBehaviour
{
    public float v_x;
    public float v_y;
    public float v_z;

    public GameObject Exp;

    public float speed;

    private GameObject player;
    private Vector3 _dir;

    private float rand;

    private bool bulletBool = true;

    private float ac = 1f;

    [SerializeField] private Vector3 _forward = Vector3.forward;

    Quaternion lookAtRotation;
    Quaternion offsetRotation;
    private Vector3 dir;

    void Start()
    {

        player = GameObject.FindGameObjectWithTag("target");
        // transform.LookAt(player.transform);
        var dir = player.transform.position - transform.position;
        var lookAtRotation = Quaternion.LookRotation(dir, Vector3.up);
        var offsetRotation = Quaternion.FromToRotation(_forward, Vector3.forward);
        // offsetRotation += Quaternion.Euler(0f, 90f, 0f);
        transform.rotation = lookAtRotation * offsetRotation * Quaternion.Euler(-90f, 0f, 0f);
        // transform.rotation = Quaternion.Euler(-90f, 0f, 0f);

    }

    public void SetBullet(float x, float y, float z)
    {
        v_x = x;
        v_y = y;
        v_z = z;
    }

    void Update()
    {
        StartCoroutine("BulletAttack");
    }

    IEnumerator BulletAttack()
    {
        yield return new WaitForSeconds(3f);
        if(bulletBool)
        {

            player = GameObject.Find("Player");
            // transform.LookAt(player.transform);
            var dir = player.transform.position - transform.position;
            var lookAtRotation = Quaternion.LookRotation(dir, Vector3.up);
            var offsetRotation = Quaternion.FromToRotation(_forward, Vector3.forward);
            // offsetRotation += Quaternion.Euler(0f, 90f, 0f);
            transform.rotation = lookAtRotation * offsetRotation * Quaternion.Euler(-90f, 0f, 0f);
            // transform.rotation = Quaternion.Euler(-90f, 0f, 0f);

            _dir = (player.transform.position - transform.position).normalized;
            _dir.y += 0.05f;
            // _dir = player.transform.position - transform.position;
            bulletBool = false;
        }
        transform.position += new Vector3(speed * _dir.x * Time.deltaTime * ac, speed * _dir.y * Time.deltaTime * ac, speed * _dir.z * Time.deltaTime *ac);
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, lookAtRotation * offsetRotation * Quaternion.Euler(-90f, 0f, 0f), 1f);

        ac += 3 * Time.deltaTime;

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

        if (other.gameObject.tag == "CS")
        {
            Destroy(gameObject);

            rand = Random.Range(0f, 1f);

            if (rand >= 0.5f)
            {
                GameObject exp = Instantiate(Exp, transform.position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;

                EController expScript = exp.GetComponent<EController>();

                expScript.setVelocity(speed * _dir.x, speed * _dir.y, speed * _dir.z);
            }
        }
    }
}
