using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    public AlertSystem alertSystem;
    public GameLogic gameLogic;  // Reference to GameLogic


    void Start()
    {
        // 获取 GameLogic 的引用
        gameLogic = FindObjectOfType<GameLogic>();
    }

    public void InteractWithTarget()
    {
        // 当玩家与目标对话时，增加警惕值 +20
        float newAlertValue = alertSystem.IncreaseAlert(20f);
        Debug.Log("当前警惕值: " + newAlertValue);
    }


    // 射击目标
    public void ShootTarget()
    {
        // 当玩家开枪时，警惕值增加 +100
        float newAlertValue = alertSystem.IncreaseAlert(100f);
        Debug.Log("当前警惕值: " + newAlertValue);
    }

    // 停电
    public void Blackout()
    {
        // 当玩家关闭电闸导致停电时，警惕值增加 +30
        float newAlertValue = alertSystem.IncreaseAlert(30f);
        Debug.Log("当前警惕值: " + newAlertValue);
    }

    // 与玻璃交互
    public void InteractWithGlass()
    {
        if (gameLogic == null)
        {
            Debug.LogError("GameLogic not assigned!");
            return;
        }

        // 只有在 lightsOff 状态下，交互玻璃才会触发胜利
        if (gameLogic.lightsOff)
        {
            Debug.Log("Player interacted with glass during lights off. Victory!");
            gameLogic.LoadVictoryScene();  // 调用 GameLogic 中的加载胜利场景的方法
        }
        else
        {
            Debug.Log("Player interacted with glass, but lights are still on. No victory yet.");
        }
    }

    // 与NPC交互时不增加警惕值
    public void TalkToNPC()
    {
        Debug.Log("与 NPC 对话，警惕值不增加");
    }

    // 与BackDoor交互时不增加警惕值
    public void InteractWithBackDoor()
    {
        Debug.Log("与 BackDoor 交互，警惕值不增加");
    }
}
