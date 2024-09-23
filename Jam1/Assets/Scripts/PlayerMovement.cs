using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // movespeed
    private Rigidbody rb;      // rigidbody 3d
    private Vector3 movement;    // direct
    public GameLogic gameLogic;


    public UIButtonController uiButtonController; 
    public PlayerActions playerActions;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // ��ȡ3������
        uiButtonController = FindObjectOfType<UIButtonController>();

        gameLogic = FindObjectOfType<GameLogic>();

        playerActions = FindObjectOfType<PlayerActions>();

        if (gameLogic != null)
        {
            Debug.Log("GameLogic found and initialized.");
        }
        else
        {
            Debug.LogError("GameLogic not found! Ensure it's attached to an object in the scene.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // input,A  is  -1, D  is  1
        movement.x = Input.GetAxisRaw("Horizontal");
    }

    void FixedUpdate()
    {
        // �ƶ�
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }





    //activate buttons
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            uiButtonController.ActivateShootButton();
            uiButtonController.ActivateTalkButton();
        }
        else if (other.CompareTag("Glass"))
        {
            uiButtonController.ActivateInteractButton();
        }
        else if (other.CompareTag("Gate"))
        {
            uiButtonController.ActivateInteractButton();
        }
        else if (other.CompareTag("BackDoor"))
        {
            if (gameLogic.talkedToNPC)
            {
                uiButtonController.ActivateInteractButton();
                Debug.Log("BackDoor Activated!");
            }
            else
            {
                Debug.Log("BackDoor interaction not available yet, talk to NPC first.");
            }
        }
        else if (other.CompareTag("NPC"))
        {
            uiButtonController.ActivateTalkButton();
        }
        else if (other.CompareTag("BackYard"))
        {
            // ������� BackYard ʱ���������ť
            uiButtonController.ActivateInteractButton();
            Debug.Log("Player is in BackYard, InteractButton activated!");
        }
    }







    // ��ײ�뿪ʱ����ť�ָ�Ϊ������״̬
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            uiButtonController.DeactivateShootButton();
            uiButtonController.DeactivateTalkButton();
        }
        else if (other.CompareTag("Glass") || other.CompareTag("Gate") || other.CompareTag("BackDoor") || other.CompareTag("BackYard"))
        {
            uiButtonController.DeactivateInteractButton();
        }
        else if (other.CompareTag("NPC"))
        {
            uiButtonController.DeactivateTalkButton();
        }
    }


}
 