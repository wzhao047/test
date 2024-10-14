using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // ��Ҷ����Transform
    public Vector3 offset;     // �������ҵ�ƫ����

    void Start()
    {
        // ���������������ʼƫ��������������Ϊ�������ҵ�ǰ�����λ��
        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        // �������λ�ã���������ҵ�ƫ��
        transform.position = player.position + offset;
    }
}
