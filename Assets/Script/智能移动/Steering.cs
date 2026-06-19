using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steering
{
    public Vector3 linear;//加速度
    public float angular;//角速度

    public Steering()
    {
        linear = Vector3.zero;
        angular = 0;
    }
}