using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sika : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // プレイヤーを sika の親オブジェクトの子にする
            collision.transform.SetParent(transform.parent);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // 親子関係を解除する
        collision.transform.SetParent(null);
    }
}