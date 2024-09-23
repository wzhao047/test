using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonController : MonoBehaviour
{
    // public GameObject and Image object of ui
    public GameObject interactButton;
    public GameObject shootButton;
    public GameObject talkButton;

    public GameObject interactNoButton;
    public GameObject shootNoButton;
    public GameObject talkNoButton;

    // Start is called before the first frame update
    void Start()
    {
        // ��ʾ�����ð�ť�����ؿ��ð�ť
        interactButton.SetActive(false);
        shootButton.SetActive(false);
        talkButton.SetActive(false);

        interactNoButton.SetActive(true);
        shootNoButton.SetActive(true);
        talkNoButton.SetActive(true);
    }

    // �����Ӧbutton
    public void ActivateShootButton()
    {
        shootNoButton.SetActive(false);
        shootButton.SetActive(true);
    }

    public void ActivateInteractButton()
    {
        interactNoButton.SetActive(false);
        interactButton.SetActive(true);
    }

    public void ActivateTalkButton()
    {
        talkNoButton.SetActive(false);
        talkButton.SetActive(true);
    }





    // ����ײʱ�ָ���ť������
    public void DeactivateShootButton()
    {
        shootButton.SetActive(false);
        shootNoButton.SetActive(true);
    }

    public void DeactivateInteractButton()
    {
        interactButton.SetActive(false);
        interactNoButton.SetActive(true);
    }

    public void DeactivateTalkButton()
    {
        talkButton.SetActive(false);
        talkNoButton.SetActive(true);
    }







    // �ڰ�ť����ʱִ�еĹ���
    public void ShootTarget()
    {
        Debug.Log("Target Got Shoot");
    }

    public void InteractWithObject(string objectName)
    {
        Debug.Log(objectName + " Touched");
    }

    public void TalkToNPC()
    {
        Debug.Log("Talk to NPCs");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
