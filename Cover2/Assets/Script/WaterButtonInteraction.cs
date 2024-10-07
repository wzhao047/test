using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterButtonInteraction : MonoBehaviour
{
    public GameObject waterDoorObstacle;  // 水的障碍物

    // 当WaterAgent与按钮碰撞时触发
    private void OnTriggerEnter(Collider other)
    {
        // 判断碰撞物体是否是WaterAgent
        if (other.CompareTag("WaterAgent"))
        {
            // 销毁NavMeshObstacle对象
            Destroy(waterDoorObstacle);

            // 输出调试信息
            Debug.Log("WaterAgent triggered the button. The water door has been destroyed.");
        }
    }
}
