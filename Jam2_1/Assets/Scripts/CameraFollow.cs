using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // 玩家对象的Transform
    public Vector3 offset;     // 相机与玩家的偏移量

    void Start()
    {
        // 可以在这里调整初始偏移量，比如设置为相机与玩家当前的相对位置
        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        // 更新相机位置，保持与玩家的偏移
        transform.position = player.position + offset;
    }
}
