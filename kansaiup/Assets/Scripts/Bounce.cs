using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    private Coroutine jumpCoroutine;
    [SerializeField] private float bounceForce;
    [SerializeField] private float delayTime = 2f;


    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            if(jumpCoroutine != null)
            {
                StopCoroutine(jumpCoroutine);
            }
            jumpCoroutine = StartCoroutine(Jump(rb));
        }
    }

    private void OnCollisionExit(Collision other) {
        if(other.gameObject.CompareTag("Player"))
        {
            if(jumpCoroutine != null)
            {
                StopCoroutine(jumpCoroutine);
                jumpCoroutine = null;    
            }
        }
    }

    IEnumerator Jump(Rigidbody rb)
    {
        yield return new WaitForSeconds(delayTime);
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z); // 縦の速度をリセット
        rb.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
    }
}
