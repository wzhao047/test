using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // �˺������ڰ�ť���ʱ����
    public void LoadMainScene()
    {
        // ���� MainScene ����
        SceneManager.LoadScene("MainScene");
    }
}
