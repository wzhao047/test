using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  // �����һ����ʹ�� SceneManager
using TMPro;  // ����TextMeshPro�����ռ�




public class GameLogic : MonoBehaviour
{
    // �����е���Ҫ����
    public GameObject backdoor;
    public GameObject backYardOff; // ͣ���ĺ�Ժ
    public GameObject barRoomOff;  // ͣ���ľư�
    public Transform backYardParent;
    public Transform barRoomParent;
    public Transform playerTransform;       // ���
    public Transform backYardOffPosition;   // ͣ���ĺ�Ժλ��
    public Transform barRoomOffPosition;    // �ư�ͣ����λ��


    // ������Һ�����ĳ�ʼλ��
    private Vector3 previousPlayerPosition;
    private Vector3 previousCameraPosition;

    // ��Ϸ״̬���
    public bool talkedToNPC = false;    // �������Ƿ��� NPC ��̸
    public bool lightsOff = false;      // ��ǹص�״̬
    private bool gameCompleted = false;

    public GameObject dialogueTextBox;   // �Ի���
    public TMP_Text dialogueText;        // �Ի����е��ı�


    // ��ʼ��
    void Start()
    {
        // ȷ��BackDoor����Ϸ��ʼʱ������
        backdoor.SetActive(false);

        if (dialogueTextBox == null || dialogueText == null)
        {
            Debug.LogError("dialogueTextBox or dialogueText is not assigned in the Inspector!");
            return;
        }

        dialogueTextBox.SetActive(false);  // ��Ϸ��ʼʱ���ضԻ���
    }

    public void TalkToNPC()
    {
        // ��ʾ NPC �Ի�
        dialogueTextBox.SetActive(true);
        dialogueText.text = "Last week, someone deliberately turned off the switch at the back door of the residential building next door, causing a power outage in the entire building.";

    }

    private IEnumerator HideNPCTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        dialogueTextBox.SetActive(false); // ���ضԻ���
        Debug.Log("NPC text hidden.");
    }

    // ��Һͺ��ţ�BackDoor���������ƶ�����Ժ
    public void InteractWithBackDoor()
    {
        Debug.Log("Interacting with BackDoor. Moving to BackYard...");

        // ����ԭʼλ��
        previousPlayerPosition = playerTransform.position;
        previousCameraPosition = Camera.main.transform.position;

        // �ƶ���Һ������ BackYardOff ��λ��
        playerTransform.position = backYardOffPosition.position;
        Camera.main.transform.position = new Vector3(backYardOffPosition.position.x, backYardOffPosition.position.y, previousCameraPosition.z);

        Debug.Log("Player and Camera moved to BackYard.");
    }

    // ����ں�Ժ����LightsOff�߼�
    public void InteractWithBackYard()
    {
        Debug.Log("Player interacted with BackYard. Lights off sequence starts!");
        LightsOff();
    }

    // ����صƺ���߼�
    public void LightsOff()
    {
        if (!lightsOff)  // ȷ�� lightsOff ִֻ��һ��
        {
            lightsOff = true;
            Debug.Log("Lights are now off. Interactions will no longer trigger alerts.");

            // �滻 BarRoom �� BackYard Ϊ�صƺ��״̬
            ReplaceGameObjects();

            // ��ʼ����ʱ������� 5 ���ص��ư�
            StartCoroutine(BackToBarRoomOffAfterDelay(5f));

            // ��ʼ 15 �뵹��ʱ����δ�����Ϸ�������ʧ�ܳ���
            StartCoroutine(LightsOffCountdown(15f));
        }
        else
        {
            Debug.Log("Lights are already off, skipping replacement.");
        }
    }

    // �滻����ͺ�ԺΪͣ����״̬
    void ReplaceGameObjects()
    {
        // ���ٵ�ǰ�� BackYard �� BarRoom ���󣬲��滻��ͣ����״̬
        if (backYardOff && barRoomOff)
        {
            // ����Ҫ�ĵط�ʵ�����������
            Instantiate(backYardOff, backYardOffPosition.position, Quaternion.identity, backYardParent);
            Instantiate(barRoomOff, barRoomOffPosition.position, Quaternion.identity, barRoomParent);
        }
        else
        {
            Debug.LogError("BackYardOff or BarRoomOff is not assigned in the Inspector.");
        }
    }

    // ��ҵ���ʱ�����󷵻ص��ư�
    IEnumerator BackToBarRoomOffAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // ��Һ�������ص��ưɵ�ͣ��״̬
        playerTransform.position = barRoomOffPosition.position;
        Camera.main.transform.position = new Vector3(barRoomOffPosition.position.x, barRoomOffPosition.position.y, previousCameraPosition.z);

        Debug.Log("Player and Camera returned to BarRoomOff after 5 seconds.");
    }

    // ����ʱ���������ʧ�ܳ���
    IEnumerator LightsOffCountdown(float countdownTime)
    {
        yield return new WaitForSeconds(countdownTime);

        if (!GameCompleted()) // ����Ϸδ��ɣ�����ʧ�ܳ���
        {
            LoadTimeoutScene();
        }
    }

    void LoadTimeoutScene()
    {
        Debug.Log("Mission failed! Loading timeout scene...");
        SceneManager.LoadScene("TimesOutScene");
    }

    // �벣������ʱ�ж�ʤ��
    public void InteractWithGlass()
    {
        // ֻ���ڹص�״̬�½�������ʤ��
        if (lightsOff)
        {
            Debug.Log("Player interacted with glass during lights off. Victory!");
            LoadVictoryScene();
        }
        else
        {
            Debug.Log("Player interacted with glass, but lights are still on. No victory yet.");
        }
    }

    // ����ʤ������
    public void LoadVictoryScene()
    {
        Debug.Log("Loading EndScene. Game won!");
        SceneManager.LoadScene("EndScene");
    }

    bool GameCompleted()
    {
        return gameCompleted;
    }
}
