using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public float maxSpeed;
    public Vector3 velocity;
    public float rotation;

    private void Start()
    {
        velocity = Vector3.zero;
    }
    private void Update()
    {
        Vector3 displacement = velocity * Time.deltaTime;
        transform.Translate(displacement, Space.World);
    }
}
