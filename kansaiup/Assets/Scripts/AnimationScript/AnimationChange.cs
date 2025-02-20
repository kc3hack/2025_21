using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationChange : MonoBehaviour
{
    public Animator animator;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetBool("anim", true);
        }
    }

    // アニメーションの最後で呼び出す
    public void ResetBlRot()
    {
        animator.SetBool("anim", false);
    }
}