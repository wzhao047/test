using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInteraction : MonoBehaviour
{
    public GameObject doorObstacle;  // 门的障碍物

    // 当FireAgent与按钮碰撞时触发
    private void OnTriggerEnter(Collider other)
    {
        // 判断碰撞物体是否是FireAgent
        if (other.CompareTag("FireAgent"))
        {
            // 销毁NavMeshObstacle对象
            Destroy(doorObstacle);

            // 输出调试信息
            Debug.Log("FireAgent triggered the button. The door has been destroyed.");
        }
    }
}
