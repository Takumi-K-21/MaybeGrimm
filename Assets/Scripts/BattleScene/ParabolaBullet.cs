using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolaBullet : MonoBehaviour
{
    private Transform endPos;  //�I�_���W
    [SerializeField] float flightTime = 2;  //�؋󎞊�
    [SerializeField] float speedRate = 1;   //�؋󎞊Ԃ���Ƃ����ړ����x�{��
    private const float gravity = -9.8f;    //�d��

    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("target");
        endPos = player.transform;
        StartCoroutine(Jump(endPos.position, flightTime, speedRate, gravity));
    }

    void Update()
    {
        if (transform.position.y > 300) Destroy(gameObject);
        if (transform.position.x < -300 || transform.position.x > 300) Destroy(gameObject);
        if (transform.position.z < -300 || transform.position.z > 300) Destroy(gameObject);
    }

    // ���݈ʒu����endPos�ւ̕����^���@
    private IEnumerator Jump(Vector3 endPos, float flightTime, float speedRate, float gravity)
    {

        var startPos = transform.position; // �����ʒu
        var diffY = (endPos - startPos).y; // �n�_�ƏI�_��y�����̍���
        var vn = (diffY - gravity * 0.5f * flightTime * flightTime) / flightTime; // ���������̏����xvn

        // �����^��
        for (var t = 0f; t < flightTime; t += (Time.deltaTime * speedRate))
        {
            var p = Vector3.Lerp(startPos, endPos, t / flightTime);   //���������̍��W�����߂� (x,z���W)
            p.y = startPos.y + vn * t + 0.5f * gravity * t * t; // ���������̍��W y
            transform.position = p;
            yield return null; //1�t���[���o��
        }

        // �I�_���W�֕␳
        transform.position = endPos;
        transform.position += endPos;

    }
}
