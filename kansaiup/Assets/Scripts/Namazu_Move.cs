using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Namazu_Move : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player")){
        {
            transform.position = new Vector3(transform.position.x + 1,transform.position.y,transform.position.z);
        }
        }
    }
}
