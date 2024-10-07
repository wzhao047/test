using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CarryableObject : MonoBehaviour
{
    private List<GameObject> carryingAgents = new List<GameObject>();  // ����С���Agent�б�
    public GameObject lockPrefab; // ��ʾ����״̬��Prefab
    private Dictionary<GameObject, GameObject> lockIndicators = new Dictionary<GameObject, GameObject>();  // Agent��������ָʾ����ӳ��

    private bool isBeingCarried = false;  // �Ƿ����ڱ�����

    // TextMeshPro UI Ԫ��
    public TextMeshProUGUI requirementText;
    public TextMeshProUGUI winText;  // Win Text

    // ��¼����״̬
    private int fireAgentRequired = 1;
    private int waterAgentRequired = 1;
    private int electricAgentRequired = 1;

    void Start()
    {
        // ��ʼ��������ʾ
        UpdateRequirementText();

        // ��ʼ����Win Text
        winText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // ����Ƿ����յ������ײ
        if (other.CompareTag("Finish"))  // �����㽫�յ���������� "Finish" ��ǩ
        {
            if (isBeingCarried)  // ���С�򱻰���
            {
                ShowWinScreen();  // ��ʾʤ����Ļ
            }
        }

        // ȷ����ײ����FireAgent��WaterAgent��ElectricAgent
        if (other.CompareTag("FireAgent") || other.CompareTag("WaterAgent") || other.CompareTag("ElectricAgent"))
        {
            if (!carryingAgents.Contains(other.gameObject) && carryingAgents.Count < 3)
            {
                carryingAgents.Add(other.gameObject);  // �������Agent

                // ��Agentͷ������������ʾ��prefab
                GameObject lockIndicator = Instantiate(lockPrefab, other.transform);
                lockIndicator.transform.localPosition = new Vector3(0, 2, 0);  // ��������ָʾ��λ����Agentͷ��
                lockIndicators.Add(other.gameObject, lockIndicator);  // ��¼��Agent������ָʾ��

                // ��������״̬
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

                // ���� UI �ı�
                UpdateRequirementText();
            }
        }

        // ����Ƿ���������ͬ���͵�Agent
        CheckCarryStatus();
    }

    // ��ʾʤ���ı�
    private void ShowWinScreen()
    {
        winText.gameObject.SetActive(true);  // ��ʾ Win �ı�
    }

    // ����Ƿ��������������3����ͬ���Ե�Agent��
    private void CheckCarryStatus()
    {
        bool hasFire = false, hasWater = false, hasElectric = false;

        foreach (GameObject agent in carryingAgents)
        {
            if (agent.CompareTag("FireAgent")) hasFire = true;
            if (agent.CompareTag("WaterAgent")) hasWater = true;
            if (agent.CompareTag("ElectricAgent")) hasElectric = true;
        }

        // �����������ͬ��Agent�����������
        if (hasFire && hasWater && hasElectric)
        {
            isBeingCarried = true;
            Debug.Log("Object is being carried by three different agents!");
        }
    }

    // ����С���λ��
    private void Update()
    {
        if (isBeingCarried && carryingAgents.Count == 3)
        {
            Vector3 averagePosition = Vector3.zero;

            // ��ȡ����Я���ߵ�λ�õ�ƽ��ֵ�����ƶ�С��
            foreach (GameObject agent in carryingAgents)
            {
                averagePosition += agent.transform.position;
            }
            averagePosition /= carryingAgents.Count;

            // С���ƶ���Agent��ƽ��λ��
            transform.position = averagePosition;
        }

        // ����Ƿ��Ҽ����С��ȡ������
        if (Input.GetMouseButtonDown(1)) // �Ҽ����
        {
            DeselectAllAgents();
        }
    }

    // ������а󶨵�Agent
    private void DeselectAllAgents()
    {
        foreach (var agent in carryingAgents)
        {
            // �Ƴ�ÿ��Agent��������ʶ
            if (lockIndicators.ContainsKey(agent))
            {
                Destroy(lockIndicators[agent]);
            }
        }

        carryingAgents.Clear();
        lockIndicators.Clear();
        isBeingCarried = false;

        // ������������
        fireAgentRequired = 1;
        waterAgentRequired = 1;
        electricAgentRequired = 1;

        // ���� UI �ı�
        UpdateRequirementText();
    }

    // ���������ı�
    private void UpdateRequirementText()
    {
        requirementText.text = $"Need {fireAgentRequired} Fire Agent, {waterAgentRequired} Water Agent, {electricAgentRequired} Electric Agent";
    }
}
