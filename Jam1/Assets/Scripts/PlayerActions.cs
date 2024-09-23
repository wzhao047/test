using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    public AlertSystem alertSystem;
    public GameLogic gameLogic;  // Reference to GameLogic


    void Start()
    {
        // ��ȡ GameLogic ������
        gameLogic = FindObjectOfType<GameLogic>();
    }

    public void InteractWithTarget()
    {
        // �������Ŀ��Ի�ʱ�����Ӿ���ֵ +20
        float newAlertValue = alertSystem.IncreaseAlert(20f);
        Debug.Log("��ǰ����ֵ: " + newAlertValue);
    }


    // ���Ŀ��
    public void ShootTarget()
    {
        // ����ҿ�ǹʱ������ֵ���� +100
        float newAlertValue = alertSystem.IncreaseAlert(100f);
        Debug.Log("��ǰ����ֵ: " + newAlertValue);
    }

    // ͣ��
    public void Blackout()
    {
        // ����ҹرյ�բ����ͣ��ʱ������ֵ���� +30
        float newAlertValue = alertSystem.IncreaseAlert(30f);
        Debug.Log("��ǰ����ֵ: " + newAlertValue);
    }

    // �벣������
    public void InteractWithGlass()
    {
        if (gameLogic == null)
        {
            Debug.LogError("GameLogic not assigned!");
            return;
        }

        // ֻ���� lightsOff ״̬�£����������Żᴥ��ʤ��
        if (gameLogic.lightsOff)
        {
            Debug.Log("Player interacted with glass during lights off. Victory!");
            gameLogic.LoadVictoryScene();  // ���� GameLogic �еļ���ʤ�������ķ���
        }
        else
        {
            Debug.Log("Player interacted with glass, but lights are still on. No victory yet.");
        }
    }

    // ��NPC����ʱ�����Ӿ���ֵ
    public void TalkToNPC()
    {
        Debug.Log("�� NPC �Ի�������ֵ������");
    }

    // ��BackDoor����ʱ�����Ӿ���ֵ
    public void InteractWithBackDoor()
    {
        Debug.Log("�� BackDoor ����������ֵ������");
    }
}
