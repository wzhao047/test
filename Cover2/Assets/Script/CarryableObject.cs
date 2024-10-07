using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CarryableObject : MonoBehaviour
{
    private List<GameObject> carryingAgents = new List<GameObject>();  // 搬运小球的Agent列表
    public GameObject lockPrefab; // 显示锁定状态的Prefab
    private Dictionary<GameObject, GameObject> lockIndicators = new Dictionary<GameObject, GameObject>();  // Agent到其锁定指示器的映射

    private bool isBeingCarried = false;  // 是否正在被搬运

    // TextMeshPro UI 元素
    public TextMeshProUGUI requirementText;
    public TextMeshProUGUI winText;  // Win Text

    // 记录搬运状态
    private int fireAgentRequired = 1;
    private int waterAgentRequired = 1;
    private int electricAgentRequired = 1;

    void Start()
    {
        // 初始化需求显示
        UpdateRequirementText();

        // 初始隐藏Win Text
        winText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // 检查是否与终点地面碰撞
        if (other.CompareTag("Finish"))  // 假设你将终点地面设置了 "Finish" 标签
        {
            if (isBeingCarried)  // 如果小球被搬运
            {
                ShowWinScreen();  // 显示胜利屏幕
            }
        }

        // 确保碰撞物是FireAgent、WaterAgent或ElectricAgent
        if (other.CompareTag("FireAgent") || other.CompareTag("WaterAgent") || other.CompareTag("ElectricAgent"))
        {
            if (!carryingAgents.Contains(other.gameObject) && carryingAgents.Count < 3)
            {
                carryingAgents.Add(other.gameObject);  // 加入搬运Agent

                // 在Agent头上生成锁定显示的prefab
                GameObject lockIndicator = Instantiate(lockPrefab, other.transform);
                lockIndicator.transform.localPosition = new Vector3(0, 2, 0);  // 设置锁定指示器位置在Agent头上
                lockIndicators.Add(other.gameObject, lockIndicator);  // 记录该Agent的锁定指示器

                // 更新需求状态
                if (other.CompareTag("FireAgent") && fireAgentRequired > 0)
                {
                    fireAgentRequired--;
                }
                else if (other.CompareTag("WaterAgent") && waterAgentRequired > 0)
                {
                    waterAgentRequired--;
                }
                else if (other.CompareTag("ElectricAgent") && electricAgentRequired > 0)
                {
                    electricAgentRequired--;
                }

                // 更新 UI 文本
                UpdateRequirementText();
            }
        }

        // 检查是否有三个不同类型的Agent
        CheckCarryStatus();
    }

    // 显示胜利文本
    private void ShowWinScreen()
    {
        winText.gameObject.SetActive(true);  // 显示 Win 文本
    }

    // 检查是否满足搬运条件（3个不同属性的Agent）
    private void CheckCarryStatus()
    {
        bool hasFire = false, hasWater = false, hasElectric = false;

        foreach (GameObject agent in carryingAgents)
        {
            if (agent.CompareTag("FireAgent")) hasFire = true;
            if (agent.CompareTag("WaterAgent")) hasWater = true;
            if (agent.CompareTag("ElectricAgent")) hasElectric = true;
        }

        // 如果有三个不同的Agent，则允许搬运
        if (hasFire && hasWater && hasElectric)
        {
            isBeingCarried = true;
            Debug.Log("Object is being carried by three different agents!");
        }
    }

    // 更新小球的位置
    private void Update()
    {
        if (isBeingCarried && carryingAgents.Count == 3)
        {
            Vector3 averagePosition = Vector3.zero;

            // 获取所有携带者的位置的平均值，并移动小球
            foreach (GameObject agent in carryingAgents)
            {
                averagePosition += agent.transform.position;
            }
            averagePosition /= carryingAgents.Count;

            // 小球移动到Agent的平均位置
            transform.position = averagePosition;
        }

        // 检查是否右键点击小球，取消锁定
        if (Input.GetMouseButtonDown(1)) // 右键点击
        {
            DeselectAllAgents();
        }
    }

    // 解除所有绑定的Agent
    private void DeselectAllAgents()
    {
        foreach (var agent in carryingAgents)
        {
            // 移除每个Agent的锁定标识
            if (lockIndicators.ContainsKey(agent))
            {
                Destroy(lockIndicators[agent]);
            }
        }

        carryingAgents.Clear();
        lockIndicators.Clear();
        isBeingCarried = false;

        // 重置需求数量
        fireAgentRequired = 1;
        waterAgentRequired = 1;
        electricAgentRequired = 1;

        // 更新 UI 文本
        UpdateRequirementText();
    }

    // 更新需求文本
    private void UpdateRequirementText()
    {
        requirementText.text = $"Need {fireAgentRequired} Fire Agent, {waterAgentRequired} Water Agent, {electricAgentRequired} Electric Agent";
    }
}
