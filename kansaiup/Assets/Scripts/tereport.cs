using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tereport : MonoBehaviour
{
    public GameObject gb;
    // Start is called before the first frame update
    void Start()
    {
               
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.transform.position = gb.transform.position;
            Debug.Log("触れた");
        }
     }
}
