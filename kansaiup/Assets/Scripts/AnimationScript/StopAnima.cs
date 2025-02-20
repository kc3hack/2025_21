using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopAnima : MonoBehaviour
{
    // アニメーションの最後で呼び出す
    public Animator animator;
    public void ResetBlRot()
    {
        animator.SetBool("anim", false);
    }
}
