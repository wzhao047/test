using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    void Update()
    {
        // 监听按键R
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    // 重启游戏方法
    void RestartGame()
    {
        // 重新加载当前场景
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
