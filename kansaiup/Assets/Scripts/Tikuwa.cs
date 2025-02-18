using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Tikuwa : MonoBehaviour
{
    private Renderer objectRenderer;
    private Collider objectCollider;
    [SerializeField] private float disappearaceTime;
    [SerializeField] private float appearaceTime;
    // Start is called before the first frame update
    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        objectCollider = GetComponent<Collider>();
    }

void OnCollisionEnter(Collision collision)
{
    if (collision.gameObject.CompareTag("Player"))
    {
        StartCoroutine(Disappearance());
    }
}

IEnumerator Disappearance()
{
        yield return new WaitForSeconds(disappearaceTime);
        // オブジェクトを見た目上非表示にする
        objectRenderer.enabled = false;
        objectCollider.enabled = false;

        // 3秒待機
        yield return new WaitForSeconds(appearaceTime);

        // オブジェクトを再度表示する
        objectCollider.enabled = true;
        objectRenderer.enabled = true;
}

}
