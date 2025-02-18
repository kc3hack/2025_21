using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(disappearance());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator disappearance()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
