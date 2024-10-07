using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 10f; // ������ƶ��ٶ�
    public float rotationSpeed = 100f; // �������ת�ٶ�

    private Vector3 initialPosition = new Vector3(0, 10, -20); // ��ĳ�ʼλ��
    private Quaternion initialRotation = Quaternion.Euler(20, 0, 0); // ��ĳ�ʼ��ת

    void Start()
    {
        // ���ó�ʼλ�ú���ת
        transform.position = initialPosition;
        transform.rotation = initialRotation;
    }

    void Update()
    {
        // WASD ������������ƶ�
        float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float moveZ = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        transform.Translate(new Vector3(moveX, 0, moveZ), Space.World);

        // Q/E �������������ת
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
        }

        // Tab���ָ���ʼλ��
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ResetCameraPosition();
        }
    }

    void ResetCameraPosition()
    {
        // �ָ����趨�ĳ�ʼλ�ú���ת
        transform.position = initialPosition;
        transform.rotation = initialRotation;
    }
}
