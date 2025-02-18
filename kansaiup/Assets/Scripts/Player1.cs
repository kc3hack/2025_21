using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    Rigidbody rb;
    Animator animator;
    Quaternion targetRotation;
    GameManager GM;
    public LayerMask groundLayer;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpPower = 10f;
    [SerializeField] private float checkDistance = 0.3f;

    private bool isJump = true;
    private bool isIce = false;

    void Awake()
    {
        //コンポーネントを関連付け
        TryGetComponent(out animator);

    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        GM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //入力ベクトルの取得
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var horizontalRotation = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up);
        var velocity = horizontalRotation * new Vector3(horizontal,0,vertical).normalized;

        //速度の取得
        var speed = Input.GetKey(KeyCode.LeftShift) ? 2: 1;
        var rotationSpeed = 600 * Time.deltaTime;

        //移動
        if(isIce == true)
        {
            // カメラの向きを基準にした移動方向の計算
            Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
            Vector3 cameraRight = Camera.main.transform.right;
            Vector3 moveDirection = cameraForward * vertical + cameraRight * horizontal;

            moveDirection = moveDirection.normalized; // 正規化して移動方向を統一
            rb.AddForce(moveDirection * moveSpeed * 0.1f, ForceMode.Force);
        }else if(isIce == false)
        {
            transform.position += velocity * moveSpeed * Time.deltaTime;
        }
        
        //移動方向を向く
        if(velocity.magnitude > 0.5f)
        {
            transform.rotation = Quaternion.LookRotation(velocity, Vector3.up);
        }
        
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed);
        

        //移動速度をAnimatorに反映
        animator.SetFloat("Speed", velocity.magnitude * speed, 0.1f, Time.deltaTime);


        //地面との当たり判定をレイキャストで行う
        Vector3 rayOrigin = transform.position + Vector3.up * 0.2f; // 少し上から Ray を飛ばす
        isJump = Physics.Raycast(rayOrigin, Vector3.down, checkDistance, groundLayer);

        //ジャンプ
        if(isJump == true && Input.GetKeyDown(KeyCode.Space) && !animator.GetCurrentAnimatorStateInfo(0).IsName("mixamo_com"))
        {
            Jump(jumpPower);
        }

        //リスポーン
        Deth();

    }


    private void OnCollisionEnter(Collision other) {
        
        if(other.gameObject.CompareTag("Ice"))
        {
            isIce = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ice"))
        {
            isIce = false;
        }
    }


    private void OnDrawGizmos()
    {
        // シーンビューで Ray を可視化
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * checkDistance);
    }

    

    public void Jump(float jumpPower)
    {
        rb.AddForce(transform.up * jumpPower, ForceMode.Impulse);
        animator.SetTrigger("Jump");
    }

    public void Deth()
    {
        if(transform.position.y <= 1)
        {
            transform.position = GM.CheckPointPos;
        }
    }

    }
