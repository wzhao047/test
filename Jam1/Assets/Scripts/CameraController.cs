using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera mainCamera;
    public Transform backYardPosition;
    public Transform player;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveToBackdoor()
    {
        if (backYardPosition != null)
        {
            // 移动相机到后院位置
            mainCamera.transform.position = new Vector3(backYardPosition.position.x, backYardPosition.position.y, mainCamera.transform.position.z);
            Debug.Log("Moving to backdoor...");

            // 移动玩家到后院位置
            player.position = backYardPosition.position;
        }
        else
        {
            Debug.LogError("BackYard position is missing!");
        }
    }
}
