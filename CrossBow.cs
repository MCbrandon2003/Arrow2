using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossBow : MonoBehaviour
{
    public float mouseSensitivity = 100.0f;
    public Transform playerBody;
    private float xRotation = 0.0f;
    float force;
    const float maxForce = 0.5f;  // 最大力量
    const float chargeRate = 0.1f; // 每0.3秒蓄力的量
    Animator animator;
    float mouseDownTime;
    bool isCharging;
    public Slider Powerslider;
    public int speed;
    public bool ready_to_shoot;
    public int shots;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        animator = GetComponent<Animator>();
        ready_to_shoot = false;
        shots = 0;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, 0f, 180f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

        //控制水平移动
        float H = Input.GetAxis("Horizontal");
        float V = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(H, 0.0f, V);
        movement = Camera.main.transform.rotation * movement * speed;
        Rigidbody rb = playerBody.GetComponent<Rigidbody>();
        //Debug.Log(movement);
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);
        if(Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y + 3, rb.velocity.z);
        }
        if(!ready_to_shoot)
        {
            return;
        }
        //按照鼠标按下的时间蓄力，每0.3秒蓄0.1的力（最多0.5)加到animator的power属性上，并用相应的力射箭
        if (Input.GetMouseButtonDown(0)) // 0表示鼠标左键
        {
            mouseDownTime = Time.time;  // 记录鼠标按下的时间
            isCharging = true;  // 开始蓄力
            Powerslider.gameObject.SetActive(true);
        }

        if (isCharging)
        {
            float holdTime = Time.time - mouseDownTime; // 计算鼠标按下的时间
            force = Mathf.Min(holdTime / 0.3f * chargeRate, maxForce); // 计算蓄力的量，最大为0.5
            Powerslider.value = force / maxForce; // 更新力量条的值
            animator.SetFloat("Power", force + 0.5f);
        }

        if (Input.GetMouseButtonUp(0) && isCharging)
        {
            isCharging = false;  // 停止蓄力
            animator.SetTrigger("fire");
            Debug.Log("setrigger");
            float holdTime = Time.time - mouseDownTime;  // 计算鼠标按下的时间
            force = Mathf.Min(holdTime / 0.3f * chargeRate, maxForce);  // 计算蓄力的量，最大为0.5
            animator.SetFloat("Power", force + 0.5f);  // 将蓄力的量加到animator的power属性上
            StartCoroutine(DelayedFireCoroutine(force));
            Powerslider.value = 0;
            
        }
    }

    IEnumerator DelayedFireCoroutine(float f)
    {
        Debug.Log("Ready to fire!!");
        yield return new WaitForSeconds(0.5f);
        fire(f);
        shots--;
    }

    public void fire(float f)
    {
        // Your existing fire code
        GameObject arrow = Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Arrow"));
        Arrow aw = arrow.AddComponent<Arrow>();
        // 使用Find方法通过子对象的名字获取子对象
        Transform childTransform1 = transform.Find("mark");
        aw.transform.position = childTransform1.position;
        aw.transform.rotation = Quaternion.LookRotation(this.transform.up);
        Rigidbody arrow_db = arrow.GetComponent<Rigidbody>();
        aw.starting_point = aw.transform.position;
        arrow.tag = "Arrow";
        Debug.Log("starting_poing:" + aw.starting_point);
        arrow_db.velocity = 100 * f * this.transform.up;

        
    }
}