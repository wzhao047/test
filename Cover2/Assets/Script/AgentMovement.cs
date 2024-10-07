using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentMovement : MonoBehaviour
{
    public GameObject moveIndicatorPrefab;
    private NavMeshAgent agent;
    private GameObject currentMoveIndicator;
    private AgentSelection agentSelection; // 缓存AgentSelection的引用

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agentSelection = FindObjectOfType<AgentSelection>(); // 只查找一次
    }

    void Update()
    {
        GameObject selectedAgent = agentSelection.GetCurrentSelectedAgent();

        if (selectedAgent == this.gameObject)  // 保证只控制当前脚本挂载的Agent
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
