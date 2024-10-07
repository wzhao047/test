using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentSelection : MonoBehaviour
{
    public GameObject selectionIndicatorPrefab;
    private GameObject currentSelectedAgent;
    private GameObject currentSelectionIndicator;

    public GameObject GetCurrentSelectedAgent()
    {
        return currentSelectedAgent;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // 这里确保点击到的是正确的Agent标签
                if (hit.collider.CompareTag("FireAgent") || hit.collider.CompareTag("WaterAgent") || hit.collider.CompareTag("ElectricAgent"))
                {
                    SelectAgent(hit.collider.gameObject);
                }
                else
                {
                    DeselectAgent();
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            DeselectAgent();
        }
    }

    void SelectAgent(GameObject agent)
    {
        if (currentSelectedAgent != null)
        {
            DeselectAgent();
        }

        currentSelectedAgent = agent;

        if (currentSelectionIndicator != null)
        {
            Destroy(currentSelectionIndicator);
        }

        currentSelectionIndicator = Instantiate(selectionIndicatorPrefab, agent.transform);
        currentSelectionIndicator.transform.localPosition = new Vector3(0, 2, 0);
    }

    void DeselectAgent()
    {
        if (currentSelectedAgent != null)
        {
            if (currentSelectionIndicator != null)
            {
                Destroy(currentSelectionIndicator);
            }

            currentSelectedAgent = null;
        }
    }
}
