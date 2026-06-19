using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgramRectangle : MonoBehaviour
{
    //程序化生成正四棱锥的函数，并计算uv与法线。
    public int height;//正四棱锥高
    public int width;//正四棱锥底部四边形长

    private void Start()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        Mesh mesh = GenerateRectangularPyramid();
        meshFilter.mesh = mesh;
    }

    private Mesh GenerateRectangularPyramid()
    {
        Mesh mesh = new Mesh();
        mesh.name = "Rectangular Pyramid";

        //正四棱锥五个点的坐标
        float half = width / 2;
        Vector3[] vertices = new Vector3[5];
        vertices[0] = new Vector3(half,0,half);
        vertices[1] = new Vector3(-half,0,half);
        vertices[2] = new Vector3(-half, 0, -half);
        vertices[3] = new Vector3(half,0,-half);
        vertices[4] = new Vector3(0, height, 0);

        int[] triangles = new int[]{
            0,1,2,
            2,3,0,
            4,1,0,
            4,2,1,
            4,3,2,
            4,0,3
        };

        Vector2[] uv = new Vector2[vertices.Length];
        for(int i = 0; i < vertices.Length; i++)
        {
            uv[i] = new Vector2(vertices[i].x, vertices[i].y);
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        return mesh;
    }
}
