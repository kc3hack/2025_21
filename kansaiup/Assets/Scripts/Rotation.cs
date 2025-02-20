using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] private float rotate_x;
    [SerializeField] private float rotate_y;
    [SerializeField] private float rotate_z;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(new UnityEngine.Vector3(rotate_x,rotate_y,rotate_z));
    }
}
