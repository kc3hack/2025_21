using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class VerticalRotation : MonoBehaviour
{
    [SerializeField] private float rotate_x;
    [SerializeField] private float rotate_y;
    [SerializeField] private float rotate_z;
    
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new UnityEngine.Vector3(rotate_x,rotate_y,rotate_z));
    }
}
