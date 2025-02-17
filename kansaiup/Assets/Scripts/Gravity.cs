using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Gravity : MonoBehaviour
{
    Rigidbody rb;
    private Renderer objectRenderer;
    private Collider objectCollider;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        objectRenderer = GetComponent<Renderer>();
        objectCollider = GetComponent<Collider>();
        //initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

void OnCollisionEnter(Collision collision)
{
    if (collision.gameObject.CompareTag("Player"))
    {
        StartCoroutine(StopCoroutine());
    }
}

IEnumerator StopCoroutine()
{
        yield return new WaitForSeconds(1.0f);
        // オブジェクトを見た目上非表示にする
        objectRenderer.enabled = false;
        objectCollider.enabled = false;

        // 3秒待機
        yield return new WaitForSeconds(3.0f);

        // オブジェクトを再度表示する
        objectCollider.enabled = true;
        objectRenderer.enabled = true;
}

}
