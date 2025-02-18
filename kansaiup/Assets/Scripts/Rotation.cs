using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class VerticalRotation : MonoBehaviour
{
    [SerializeField] private float rotate_x;
    [SerializeField] private float rotate_y;
    [SerializeField] private float rotate_z;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new UnityEngine.Vector3(rotate_x,rotate_y,rotate_z));
    }
}
