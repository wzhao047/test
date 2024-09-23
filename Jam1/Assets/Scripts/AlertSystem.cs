using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AlertSystem : MonoBehaviour
{
    public Slider alertBar;       // ����ֵ������
    private float alertValue = 0f; // ��ǰ����ֵ
    public float maxAlert = 100f;  // �����ֵ

    private bool lightsOff = false; // ����Ƿ�ص�
    private bool alarmTriggered = false;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ÿ֡���¾���ֵ��
        alertBar.value = alertValue / maxAlert;

        // ��龯��ֵ�Ƿ񳬹����ֵ
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
            return alertValue; // ����صƣ��������Ӿ���ֵ
        }


        alertValue += amount;
        alertValue = Mathf.Clamp(alertValue, 0, maxAlert); // ȷ������ֵ�� 0 �� maxAlert ֮��
        Debug.Log("����ֵ����: " + amount + "����ǰ����ֵ: " + alertValue);
        return alertValue;
    }

    public void SetLightsOff(bool status)
    {
        lightsOff = status;
    }

    public void HandleAlertIncrease(float amount)
    {
        float newAlertValue = IncreaseAlert(amount); // ʹ�� return ��ȡ�¾���ֵ
        Debug.Log("����ֵ���Ӻ�Ϊ��" + newAlertValue);
    }


    // ��龯��ֵ�Ƿ�ﵽ�����ֵ
    public bool IsAlertMaxed()
    {
        if (alertValue >= maxAlert && !alarmTriggered)
        {
            TriggerAlarm();
            return true; // ���� true����ʾ����ֵ����
        }
        return false; // ���� false����ʾ����ֵδ��
    }

    // ������������Ϸʧ��
    void TriggerAlarm()
    {
        if (!alarmTriggered)
        {
            alarmTriggered = true; // ȷ������ֻ����һ��
            Debug.Log("Mission Failed: ����ֵ�ﵽ 100��1 ����л���ʧ�ܳ���...");
            StartCoroutine(LoadTimesOutSceneAfterDelay(1f)); // 1 ����л�����
        }
    }

    IEnumerator LoadTimesOutSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);  // �ȴ�1��
        SceneManager.LoadScene("TimesOutScene");  // ����ʧ�ܳ���
    }
}
    