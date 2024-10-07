using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentMovement : MonoBehaviour
{
    public GameObject moveIndicatorPrefab;
    private NavMeshAgent agent;
    private GameObject currentMoveIndicator;
    private AgentSelection agentSelection; // ����AgentSelection������

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agentSelection = FindObjectOfType<AgentSelection>(); // ֻ����һ��
    }

    void Update()
    {
        GameObject selectedAgent = agentSelection.GetCurrentSelectedAgent();

        if (selectedAgent == this.gameObject)  // ��ֻ֤���Ƶ�ǰ�ű����ص�Agent
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.CompareTag("Ground"))
                    {
                        agent.SetDestination(hit.point);

                        if (currentMoveIndicator != null)
                        {
                            Destroy(currentMoveIndicator);
                        }

                        currentMoveIndicator = Instantiate(moveIndicatorPrefab, hit.point, Quaternion.identity);
                    }
                }
            }
        }
    }
}
