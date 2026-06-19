using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgramVine : MonoBehaviour
{
    //    3¸öÇò

    //0 --- 1 --- 2
    private GameObject[] spheres;
    private Vector3[] velocitys;
    private Vector3[] forces;

    public int numOfSphere = 3;
    public GameObject spherePrefab;
    public float mass = 1f;
    public float spacing =1f;
    public float springStiffness =10f;
    public float damping = 0.1f;
    public LineRenderer lineRenderer;

    private void Start()
    {
        spheres = new GameObject[numOfSphere];
        velocitys = new Vector3[numOfSphere];
        forces = new Vector3[numOfSphere];
        for (int i = 0; i < numOfSphere; i++)
        {
            spheres[i] = Instantiate(spherePrefab, transform.position + new Vector3(i * spacing, 0, 0), Quaternion.identity);
            velocitys[i] = Vector3.zero;
        }
        lineRenderer.positionCount = numOfSphere;
    }
    private void FixedUpdate()
    {
        Vector3[] pos = new Vector3[numOfSphere];
        for(int i = 0; i < numOfSphere; i++)
        {
            if (i == 0) continue;
            Vector3 force = Vector3.zero;
            if (i > 0)
            {
                Vector3 springVector = spheres[i - 1].transform.position - spheres[i].transform.position;
                force += springStiffness * (springVector.magnitude - spacing) * springVector.normalized;
            }
            if (i < numOfSphere - 1)
            {
                Vector3 springVector = spheres[i + 1].transform.position - spheres[i].transform.position;
                force += springStiffness * (springVector.magnitude - spacing) * springVector.normalized;
            }
            forces[i] = force;
        }
        for(int i = 0; i < numOfSphere; i++)
        {
            velocitys[i] += (forces[i] / mass) * Time.fixedDeltaTime;
            velocitys[i] *= (1 - damping);
        }
        for(int i = 0; i < numOfSphere; i++)
        {
            spheres[i].transform.position += velocitys[i] * Time.fixedDeltaTime;
            pos[i] = lineRenderer.transform.InverseTransformPoint(spheres[i].transform.position);
        }
        lineRenderer.SetPositions(pos);
    }
}
