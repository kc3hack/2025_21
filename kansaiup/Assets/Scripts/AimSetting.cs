using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.UI;

public class AimSetting : MonoBehaviour
{
    public Slider xslider;
    public Slider yslider;
    public static float xValue;
    public static float yValue;

    // Update is called once per frame
    void Start()
    {

    }
    public void Update()
    {
        xValue = xslider.value;
        yValue = yslider.value;
    }

}
