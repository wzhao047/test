using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToStart : MonoBehaviour
{
    // �˺������ڰ�ť���ʱ����
    public void LoadStartScene()
    {
        // ���� MainScene ����
        SceneManager.LoadScene("StartScene");
    }
}
