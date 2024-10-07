using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterButtonInteraction : MonoBehaviour
{
    public GameObject waterDoorObstacle;  // ˮ���ϰ���

    // ��WaterAgent�밴ť��ײʱ����
    private void OnTriggerEnter(Collider other)
    {
        // �ж���ײ�����Ƿ���WaterAgent
        if (other.CompareTag("WaterAgent"))
        {
            // ����NavMeshObstacle����
            Destroy(waterDoorObstacle);

            // ���������Ϣ
            Debug.Log("WaterAgent triggered the button. The water door has been destroyed.");
        }
    }
}
