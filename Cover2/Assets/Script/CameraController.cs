using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 10f; // 摄像机移动速度
    public float rotationSpeed = 100f; // 摄像机旋转速度

    private Vector3 initialPosition = new Vector3(0, 10, -20); // 你的初始位置
    private Quaternion initialRotation = Quaternion.Euler(20, 0, 0); // 你的初始旋转

    void Start()
    {
        // 设置初始位置和旋转
        transform.position = initialPosition;
        transform.rotation = initialRotation;
    }

    void Update()
    {
        // WASD 键控制摄像机移动
        float moveX = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float moveZ = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        transform.Translate(new Vector3(moveX, 0, moveZ), Space.World);

        // Q/E 键控制摄像机旋转
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
        }

        // Tab键恢复初始位置
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ResetCameraPosition();
        }
    }

    void ResetCameraPosition()
    {
        // 恢复到设定的初始位置和旋转
        transform.position = initialPosition;
        transform.rotation = initialRotation;
    }
}
