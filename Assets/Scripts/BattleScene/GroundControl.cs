using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundControl : MonoBehaviour
{
    [SerializeField]
    private GameObject markerPrefab;

    [SerializeField]
    private GameObject bulletPrefab;

    private GameObject player;
    private GameObject enemy;
    public static GameObject[] marker = new GameObject[5];

    private float time;

    private Vector3 launcher;

    private float r;

    float x;
    float z;
    float[] tmp_x = new float[5];
    float[] tmp_z = new float[5];

    private Vector3 playerOffset;
    private Vector3 enemyOffset;

    public static bool isDelete;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("target");
        enemy = GameObject.Find("Gretel");
        time = 1.0f;
        isDelete = false;
    }

    void Update()
    {
        time += Time.deltaTime;

        if (GretelControl.isBullet)
        {
            GretelControl.isBullet = false;

            playerOffset = player.transform.position;
            enemyOffset = enemy.transform.position;
            playerOffset.y = 0.0f;
            enemyOffset.y = 0.0f;

            r = Vector3.Distance(playerOffset, enemyOffset);

            for (int i = 0; i < 5; i++)
            {
                // launcher = player.transform.position;
                int rand = Random.Range(210 + 24 * i, 234 + 24 * i);

                float rad = Mathf.PI / 180 * rand; // ’e‚ÌÅ‰‚ÌŠp“x

                x = r * Mathf.Cos(rad) + enemy.transform.position.x;
                z = r * Mathf.Sin(rad) + enemy.transform.position.z;

                tmp_x[i] = x;
                tmp_z[i] = z;

                marker[i] = Instantiate(markerPrefab, new Vector3(x, 1.0f, z), Quaternion.identity) as GameObject;

                Invoke("Shot", 1.5f);

                time = 0.0f;
            }
        }
    }

    void Shot()
    {
        StartCoroutine("rensha");
    }

    void Delete()
    {
        for(int i = 0; i < marker.Length; i++)
        {
            Destroy(marker[i]);
        }

        isDelete = false;
    }

    IEnumerator rensha()
    {
        for (int i = 0; i < 10; i++)
        {
            for(int j = 0; j < 5; j++)
            {
                Instantiate(bulletPrefab, new Vector3(tmp_x[j], -5.0f, tmp_z[j]), Quaternion.identity);
            }

            yield return new WaitForSeconds(0.05f);
        }

        Invoke("Delete", 0.5f);
    }
}
