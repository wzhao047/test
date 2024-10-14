using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    void Update()
    {
        // ��������R
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    // ������Ϸ����
    void RestartGame()
    {
        // ���¼��ص�ǰ����
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
