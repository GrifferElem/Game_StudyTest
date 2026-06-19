using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vine : MonoBehaviour
{
    private GameObject[] spheres;
    private Vector3[] velocities;
    private Vector3[] forces;
    private GameObject platform;

    public GameObject spherePrefabs;
    public LineRenderer lineRenderer;
    public int numOfSphere;
    public float spacing;
    public float springStiffness;
    public float damping;
    public float mass;

    public GameObject platformPrefab;
    public PlatformMassLoad platformMassLoad;

    private void Start()
    {
        spheres = new GameObject[numOfSphere];
        velocities = new Vector3[numOfSphere];
        forces = new Vector3[numOfSphere];
        for(int i = 0;i< numOfSphere; i++)
        {
            spheres[i] = Instantiate(spherePrefabs,transform.position+new Vector3(0,i*spacing,0),Quaternion.identity);
        }
        platform = Instantiate(platformPrefab, spheres[0].transform.position, Quaternion.identity);
        platformMassLoad = platform.GetComponent<PlatformMassLoad>();
        lineRenderer.positionCount = numOfSphere;
    }
    private void FixedUpdate()
    {
        Vector3[] pos = new Vector3[numOfSphere];
        for(int i = 0; i < numOfSphere; i++)
        {
            if (i == numOfSphere-1) continue;
            Vector3 force = Vector3.zero;
            if (i > 0)
            {
                Vector3 distance = spheres[i - 1].transform.position - spheres[i].transform.position;
                force += springStiffness * (distance.magnitude - spacing) * distance.normalized;
            }
            if (i < numOfSphere - 1)
            {
                Vector3 distance = spheres[i + 1].transform.position - spheres[i].transform.position;
                force += springStiffness * (distance.magnitude - spacing) * distance.normalized;
            }
            forces[i] = force;
        }
        forces[0] += Vector3.down * platformMassLoad.currentLoadMass * 9.8f;
        for (int i = 0; i < numOfSphere; ++i)
        {
            velocities[i] += (forces[i] / mass) * Time.fixedDeltaTime;
            velocities[i] *= (1 - damping);
        }
        for (int i = 0; i < numOfSphere; ++i)
        {
            if(i!=numOfSphere-1)
                spheres[i].transform.position += velocities[i] * Time.fixedDeltaTime;

            pos[i] = lineRenderer.transform.InverseTransformPoint(spheres[i].transform.position);
        }
        platform.transform.position = spheres[0].transform.position;
        lineRenderer.SetPositions(pos);
    }
}
