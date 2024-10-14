using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float jumpForce = 5f; 
    public float bounceForce = 10f;
    private Rigidbody rb;
    private bool isGrounded;
    private float jumpStartHeight;
    public TextMeshProUGUI gameOverText;  




    public float rayDistance = 10f;  // Raycast��������

    private bool isAttackMode = false;  // �Ƿ���빥��ģʽ






    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameOverText.gameObject.SetActive(false);  
    }

    void Update()
    {
        if (gameOverText.gameObject.activeSelf) return;  

        // move
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0.0f);

        rb.AddForce(movement * moveSpeed);

        // jump
        if (isGrounded && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)))
        {
            jumpStartHeight = transform.position.y;  // ��¼����ʱ�ĸ߶�
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        }






        // ����Ұ���E���������������ʱʹ�ü���
        if (Input.GetKeyDown(KeyCode.E))
        {
            isAttackMode = true;
            Debug.Log("E key pressed, ready to attack.");
        }

        // ���ڹ���ģʽ�°���������ʱ����������
        if (isAttackMode && Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse button clicked, attempting to attack.");
            UseSkill();
            isAttackMode = false;  // ÿ�ι������˳�����ģʽ
        }




    }

    private void OnCollisionEnter(Collision collision)
    {
        // ����Ƿ������˵���
        if(collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Wood"))
        {
            isGrounded = true;

            // ��������߶�
            float fallHeight = jumpStartHeight - transform.position.y;

            // �������߶���2��4֮�䣬��Ϸʧ��
            if (fallHeight >= 2 && fallHeight < 4)
            {
                GameOver();
            }
            // �������߶�Ϊ4�����з���
            else if (fallHeight >= 4)
            {
                Bounce();
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // �뿪����ʱ����Ϊ������Ծ
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Wood"))
        {
            isGrounded = false;
        }
    }

    void UseSkill()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  // �������λ�÷���Ray
        RaycastHit hit;

        // ��ʹ��LayerMask�������������
        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            Debug.Log("Raycast hit something: " + hit.collider.name);  // ��⵽���߻�����ĳ������

            // ������е���Wood
            if (hit.collider.CompareTag("Wood"))
            {
                Debug.Log("Hit Wood: " + hit.collider.name);  // ��⵽����Wood
                MakeWoodFall(hit.collider.gameObject);
            }
            else
            {
                Debug.Log("Raycast did not hit Wood, hit: " + hit.collider.tag);  // δ����Wood
            }
        }
        else
        {
            Debug.Log("Raycast did not hit anything.");  // ����û�л����κ�����
        }
        
    }

    // �����Һ�Wood֮���Ƿ����ϰ���
    bool IsObstacleBetweenPlayerAndWood(RaycastHit hit)
    {
        Vector3 directionToWood = hit.point - transform.position;
        RaycastHit obstacleHit;

        // ����Ray����ϰ�����Ray�����ϰ��ﲢ�Ҿ����Wood�������򷵻�true
        if (Physics.Raycast(transform.position, directionToWood, out obstacleHit, hit.distance))
        {
            if (obstacleHit.collider.CompareTag("Wood") == false)
            {
                // ��������Ĳ���Wood��˵�����ϰ���
                return true;
            }
        }
        return false;
    }

    // ��Wood����

    void MakeWoodFall(GameObject wood)
    {
        Rigidbody rb = wood.GetComponent<Rigidbody>();
        if (rb == null)
        {
            // ���Woodû��Rigidbody�����һ����ʹ������Ȼ������Ӱ��
            rb = wood.AddComponent<Rigidbody>();
        }

        // ȷ��Rigidbody��������������Ӱ�죬��ʼ����
        rb.useGravity = true;
    }





    // ��Ϸʧ�ܻ���
    void GameOver()
    {
        gameOverText.gameObject.SetActive(true);  // ��ʾ��Ϸʧ���ı�
        rb.isKinematic = true;  // ��������������ֹ��Ҽ����ƶ�
    }






    private void OnTriggerEnter(Collider other)
    {
        // �������Ƿ�����Flag
        if (other.gameObject.CompareTag("Flag"))
        {
            LoadNextScene();
        }
    }

    // ������һ�������ķ���
    void LoadNextScene()
    {
        // ��ȡ��ǰ����������
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // ������һ������
        SceneManager.LoadScene(currentSceneIndex + 1);
    }


    // ��������
    void Bounce()
    {
        rb.AddForce(new Vector3(0, bounceForce, 0), ForceMode.Impulse);  // ������
    }
}
