using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMassLoad : MonoBehaviour
{
    public float currentLoadMass;

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.rigidbody;
        currentLoadMass += rb.mass;
    }
    private void OnCollisionExit(Collision collision)
    {
        Rigidbody rb = collision.rigidbody;
        currentLoadMass -= rb.mass;
    }
}
