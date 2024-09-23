using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToStart : MonoBehaviour
{
    // 此函数将在按钮点击时调用
    public void LoadStartScene()
    {
        // 加载 MainScene 场景
        SceneManager.LoadScene("StartScene");
    }
}
