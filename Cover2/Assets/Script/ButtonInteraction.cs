using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInteraction : MonoBehaviour
{
    public GameObject doorObstacle;  // �ŵ��ϰ���

    // ��FireAgent�밴ť��ײʱ����
    private void OnTriggerEnter(Collider other)
    {
        // �ж���ײ�����Ƿ���FireAgent
        if (other.CompareTag("FireAgent"))
        {
            // ����NavMeshObstacle����
            Destroy(doorObstacle);

            // ���������Ϣ
            Debug.Log("FireAgent triggered the button. The door has been destroyed.");
        }
    }
}
