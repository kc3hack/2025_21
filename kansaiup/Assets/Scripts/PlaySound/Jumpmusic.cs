using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class JumpMusic : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip jumpClip;

    public void PlayJumpSound()
    {
        if (audioSource != null && jumpClip != null)
        {
            audioSource.PlayOneShot(jumpClip);
        }
    }
}
