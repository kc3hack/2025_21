using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAbsolute : MonoBehaviour
{
    [SerializeField] private float jumpPower;
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
        if(collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            Animator playerAnimator = collision.gameObject.GetComponent<Animator>();
            player.Jump(jumpPower);
        }
    }
}
