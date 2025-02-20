using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Photon.Pun;
using UnityEngine;
using Cinemachine;

public class Player : MonoBehaviourPunCallbacks
{
    Rigidbody rb;
    Animator animator;
    Quaternion targetRotation;
    GameManager GM;
    public LayerMask groundLayer;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float moveIceSpeed = 3f;
    [SerializeField] private float jumpPower = 10f;
    [SerializeField] private float checkDistance = 0.3f;
    [SerializeField] private float airControlMultiplier; // 空中移動の制限

    private bool isJump = true;
    private bool isIce = false;
    public CinemachineVirtualCamera virtualCamera;

    void Awake()
    {
        //コンポーネントを関連付け
        TryGetComponent(out animator);

    }
    void Start()
    {
        if (photonView.IsMine) // 所有者プレイヤーのみがカメラを操作
        {
            // Virtual Cameraを取得
            virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
            
            // プレイヤーのTransformをFollowとLookAtに設定
            virtualCamera.Follow = transform;
            virtualCamera.LookAt = transform;

            //Aim設定
            var pov = virtualCamera.GetCinemachineComponent<CinemachinePOV>();
            if(AimSetting.xValue == 0 && AimSetting.yValue == 0)
            {
                pov.m_HorizontalAxis.m_MaxSpeed = 2.0f;
                pov.m_VerticalAxis.m_MaxSpeed = 1.0f;
            }else{
                pov.m_HorizontalAxis.m_MaxSpeed = AimSetting.xValue * 2;
                pov.m_VerticalAxis.m_MaxSpeed = AimSetting.yValue * 2;
            }

            
        rb = GetComponent<Rigidbody>();
        GM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
    }
    }

    // Update is called once per frame
    void Update()
    {
        if(photonView.IsMine){
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
            rb.AddForce(moveDirection * moveIceSpeed, ForceMode.Force);
        }else if(isIce == false)
        {
            //transform.position += velocity * moveSpeed * Time.deltaTime;
            Vector3 cameraForward = Camera.main.transform.forward;
            cameraForward.y = 0;
            cameraForward = cameraForward.normalized;

            Vector3 cameraRight = Camera.main.transform.right;
            cameraRight.y = 0;
            cameraRight = cameraRight.normalized;

            Vector3 moveDirection = cameraForward * vertical + cameraRight * horizontal;
            moveDirection = moveDirection.normalized * 10;

                // 地上と空中で移動速度を分ける
            float currentMoveSpeed = isJump ? moveSpeed : moveSpeed * airControlMultiplier;

            // 現在のy軸の速度を保持
            rb.velocity = new Vector3(moveDirection.x * currentMoveSpeed, rb.velocity.y, moveDirection.z * currentMoveSpeed);
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
        //Vector3 rayOrigin = transform.position + Vector3.up * 0.2f; // 少し上から Ray を飛ばす
        //isJump = Physics.Raycast(rayOrigin, Vector3.down, checkDistance, groundLayer);

        float sphereRadius = 0.5f;
        Vector3 sphereCenter = transform.position + Vector3.down * 0.1f; // 足元あたりの中心

        Collider[] colliders = Physics.OverlapSphere(sphereCenter, sphereRadius, groundLayer);
        isJump = colliders.Length > 0;

        // 可視化（Sceneビュー用）
        Color gizmoColor = isJump ? Color.green : Color.red;
        Debug.DrawRay(sphereCenter, Vector3.down * checkDistance, gizmoColor);

        //ジャンプ
        if(isJump == true && Input.GetKeyDown(KeyCode.Space) && !animator.GetCurrentAnimatorStateInfo(0).IsName("mixamo_com"))
        {
            Jump(jumpPower);
        }

        //リスポーン
        if(transform.position.y <= -100)
        {
            Deth();
        }
    }

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

/*
    private void OnDrawGizmos()
    {
        // シーンビューで Ray を可視化
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * checkDistance);
    }
*/

    

    public void Jump(float jumpPower)
    {
        rb.AddForce(transform.up * jumpPower, ForceMode.Impulse);
        animator.SetTrigger("Jump");
    }

    public void Deth()
    {
            rb.velocity = new Vector3(0, 0, 0);
            transform.position = GM.CheckPointPos;
    }

    }
