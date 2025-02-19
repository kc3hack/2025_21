using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Namazu : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        collision.transform.SetParent(null);   
    }
}
