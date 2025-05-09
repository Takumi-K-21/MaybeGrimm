using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EController : MonoBehaviour {
    private int n;

    private float vec_x, vec_y, vec_z;

    private float y = 1f;

    private float d;

    private GameObject player;

    [SerializeField] private Vector3 _forward = Vector3.forward;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("target");
        // transform.LookAt(player.transform);
        var dir = player.transform.position - transform.position;
        var lookAtRotation = Quaternion.LookRotation(dir, Vector3.up);
        var offsetRotation = Quaternion.FromToRotation(_forward, Vector3.forward);
        // offsetRotation += Quaternion.Euler(0f, 90f, 0f);
        transform.rotation = lookAtRotation * offsetRotation * Quaternion.Euler(180f,90f, 0f);
        //transform.rotation = lookAtRotation * offsetRotation * Quaternion.Euler(0f, 0f, 0f);
    }

    public void setVelocity(float v_x, float v_y, float v_z) {
        vec_x = v_x;
        vec_y = v_y;
        vec_z = v_z;
    }

    void Update() {
        player = GameObject.FindGameObjectWithTag("target");
        d = Vector3.Distance(player.transform.position, transform.position);

        if(d >= 10)
        {
            // transformを取得
            Transform myTransform = this.transform;

            // 座標を取得
            Vector3 pos = myTransform.position;
            pos.x += vec_x * Time.deltaTime / 3;    // x座標へ0.01加算
            pos.y -= 5 * y * Time.deltaTime;
            pos.z += vec_z * Time.deltaTime / 3;    // z座標へ0.01加算

            myTransform.position = pos;  // 座標を設定
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 30f * Time.deltaTime);
        }

        if (transform.position.y < -300) Destroy(gameObject);
        if (transform.position.x < -300 || transform.position.x > 300) Destroy(gameObject);
        if (transform.position.z < -300 || transform.position.z > 300) Destroy(gameObject);
    }

    void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Player") {
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "Ground")
        {
            y = 0f;
        }
    }
}
