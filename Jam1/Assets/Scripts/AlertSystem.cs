using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AlertSystem : MonoBehaviour
{
    public Slider alertBar;       // 警惕值进度条
    private float alertValue = 0f; // 当前警惕值
    public float maxAlert = 100f;  // 最大警惕值

    private bool lightsOff = false; // 标记是否关灯
    private bool alarmTriggered = false;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 每帧更新警惕值条
        alertBar.value = alertValue / maxAlert;

        // 检查警惕值是否超过最大值
        if (alertValue >= maxAlert)
        {
            TriggerAlarm();
        }
    }

    // add alert value, return 
    public float IncreaseAlert(float amount)
    {
        if (lightsOff)
        {
            return alertValue; // 如果关灯，不再增加警惕值
        }


        alertValue += amount;
        alertValue = Mathf.Clamp(alertValue, 0, maxAlert); // 确保警惕值在 0 到 maxAlert 之间
        Debug.Log("警惕值增加: " + amount + "，当前警惕值: " + alertValue);
        return alertValue;
    }

    public void SetLightsOff(bool status)
    {
        lightsOff = status;
    }

    public void HandleAlertIncrease(float amount)
    {
        float newAlertValue = IncreaseAlert(amount); // 使用 return 获取新警惕值
        Debug.Log("警惕值增加后为：" + newAlertValue);
    }


    // 检查警惕值是否达到了最大值
    public bool IsAlertMaxed()
    {
        if (alertValue >= maxAlert && !alarmTriggered)
        {
            TriggerAlarm();
            return true; // 返回 true，表示警惕值已满
        }
        return false; // 返回 false，表示警惕值未满
    }

    // 触发警报和游戏失败
    void TriggerAlarm()
    {
        if (!alarmTriggered)
        {
            alarmTriggered = true; // 确保警报只触发一次
            Debug.Log("Mission Failed: 警惕值达到 100，1 秒后切换到失败场景...");
            StartCoroutine(LoadTimesOutSceneAfterDelay(1f)); // 1 秒后切换场景
        }
    }

    IEnumerator LoadTimesOutSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);  // 等待1秒
        SceneManager.LoadScene("TimesOutScene");  // 加载失败场景
    }
}
    