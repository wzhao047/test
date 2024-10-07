using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    void Update()
    {
        // 监听 R 键按下
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame(); // 调用重启游戏函数
        }
    }

    // 重启游戏的函数
    private void RestartGame()
    {
        // 重新加载当前场景
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
