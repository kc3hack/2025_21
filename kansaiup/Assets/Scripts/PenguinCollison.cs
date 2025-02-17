using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinCollison : MonoBehaviour
{
    [SerializeField] private float bounceForce;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
    if (collision.gameObject.CompareTag("Player"))
    {
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z); // 縦の速度をリセット

        // プレイヤーの後ろ上方向を計算
        Vector3 backwardUpDirection = -transform.forward + transform.up;
        backwardUpDirection.Normalize();

        // プレイヤーの後ろ上方向に力を加える
        rb.AddForce(backwardUpDirection * bounceForce, ForceMode.Impulse);
    }
    }
}
