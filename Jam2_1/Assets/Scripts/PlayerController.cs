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




    public float rayDistance = 10f;  // Raycast的最大距离

    private bool isAttackMode = false;  // 是否进入攻击模式






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
            jumpStartHeight = transform.position.y;  // 记录起跳时的高度
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        }






        // 当玩家按下E键并且鼠标左键点击时使用技能
        if (Input.GetKeyDown(KeyCode.E))
        {
            isAttackMode = true;
            Debug.Log("E key pressed, ready to attack.");
        }

        // 当在攻击模式下按下鼠标左键时，发射射线
        if (isAttackMode && Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse button clicked, attempting to attack.");
            UseSkill();
            isAttackMode = false;  // 每次攻击后退出攻击模式
        }




    }

    private void OnCollisionEnter(Collision collision)
    {
        // 检测是否碰到了地面
        if(collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Wood"))
        {
            isGrounded = true;

            // 计算下落高度
            float fallHeight = jumpStartHeight - transform.position.y;

            // 如果下落高度在2到4之间，游戏失败
            if (fallHeight >= 2 && fallHeight < 4)
            {
                GameOver();
            }
            // 如果下落高度为4，进行反弹
            else if (fallHeight >= 4)
            {
                Bounce();
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // 离开地面时设置为不可跳跃
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Wood"))
        {
            isGrounded = false;
        }
    }

    void UseSkill()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  // 从鼠标点击位置发射Ray
        RaycastHit hit;

        // 不使用LayerMask，检测所有物体
        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            Debug.Log("Raycast hit something: " + hit.collider.name);  // 检测到射线击中了某个物体

            // 如果击中的是Wood
            if (hit.collider.CompareTag("Wood"))
            {
                Debug.Log("Hit Wood: " + hit.collider.name);  // 检测到击中Wood
                MakeWoodFall(hit.collider.gameObject);
            }
            else
            {
                Debug.Log("Raycast did not hit Wood, hit: " + hit.collider.tag);  // 未击中Wood
            }
        }
        else
        {
            Debug.Log("Raycast did not hit anything.");  // 射线没有击中任何物体
        }
        
    }

    // 检查玩家和Wood之间是否有障碍物
    bool IsObstacleBetweenPlayerAndWood(RaycastHit hit)
    {
        Vector3 directionToWood = hit.point - transform.position;
        RaycastHit obstacleHit;

        // 发射Ray检测障碍物，如果Ray碰到障碍物并且距离比Wood更近，则返回true
        if (Physics.Raycast(transform.position, directionToWood, out obstacleHit, hit.distance))
        {
            if (obstacleHit.collider.CompareTag("Wood") == false)
            {
                // 如果碰到的不是Wood，说明有障碍物
                return true;
            }
        }
        return false;
    }

    // 让Wood掉落

    void MakeWoodFall(GameObject wood)
    {
        Rigidbody rb = wood.GetComponent<Rigidbody>();
        if (rb == null)
        {
            // 如果Wood没有Rigidbody，添加一个，使物体自然受重力影响
            rb = wood.AddComponent<Rigidbody>();
        }

        // 确保Rigidbody允许物体受重力影响，开始掉落
        rb.useGravity = true;
    }





    // 游戏失败机制
    void GameOver()
    {
        gameOverText.gameObject.SetActive(true);  // 显示游戏失败文本
        rb.isKinematic = true;  // 禁用物理交互，防止玩家继续移动
    }






    private void OnTriggerEnter(Collider other)
    {
        // 检测玩家是否碰到Flag
        if (other.gameObject.CompareTag("Flag"))
        {
            LoadNextScene();
        }
    }

    // 加载下一个场景的方法
    void LoadNextScene()
    {
        // 获取当前场景的索引
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // 加载下一个场景
        SceneManager.LoadScene(currentSceneIndex + 1);
    }


    // 反弹机制
    void Bounce()
    {
        rb.AddForce(new Vector3(0, bounceForce, 0), ForceMode.Impulse);  // 反弹力
    }
}
