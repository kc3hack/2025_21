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

    IEnumerator disappearance()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }
}
