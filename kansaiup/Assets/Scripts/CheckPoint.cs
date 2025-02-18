using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private GameManager GM;
    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            GM.CheckPointPos = transform.position;
            Debug.Log(GM.CheckPointPos);
        }
    }
}
