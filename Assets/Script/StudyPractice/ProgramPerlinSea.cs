using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgramPerlinSea : MonoBehaviour
{
    //实时地调整生成地形的顶点位置来实现动画的效果。尝试使用柏林噪声生成一片有动画的海面
    private Mesh mesh;
    private Vector3[] originalVertices;
    private Vector3[] currentVertices;

    public int width = 10, height = 10;
    public float noiseScale = 10;
    public float waveSpeed = 0.5f;
    public float waveHeight = 2f;

    private void Start()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        mesh = GeneratePlane();
        meshFilter.mesh = mesh;

        originalVertices = mesh.vertices;
        currentVertices = new Vector3[originalVertices.Length];
    }
    private void Update()
    {
        float time = Time.time * waveSpeed;
        for(int i = 0; i < originalVertices.Length; i++)
        {
            Vector3 baseV = originalVertices[i];
            float newY = GetDisplacement(baseV.x + time, baseV.z + time) * waveHeight;
            currentVertices[i] = new Vector3(baseV.x, newY, baseV.z);
        }
        mesh.vertices = currentVertices;
        mesh.RecalculateNormals();
    }

    private Mesh GeneratePlane()
    {
        Mesh m = new Mesh();
        m.name = "ScriptedMesh";

        int hCount2 = width + 1;
        int vCount2 = height + 1;
        int numTriangles = width * height * 6;
        int numVertices = hCount2 * vCount2;

        Vector3[] vertices = new Vector3[numVertices];
        Vector3[] normals = new Vector3[numVertices];
        Vector2[] uv = new Vector2[numVertices];
        int[] triangles = new int[numTriangles];

        int index = 0;
        float uvFactorX = 1 / width;
        float uvFactorY = 1 / height;
        float scaleX = width;
        float scaleY = height;
        for (float y = 0; y < vCount2; y++)
        {
            for (float x = 0; x < hCount2; x++)
            {
                vertices[index] = new Vector3(x * scaleX - width / 2f, 0, y * scaleY - height / 2f) + Vector3.up * GetDisplacement(x, y) * scaleY;
                normals[index] = Vector3.up;
                uv[index++] = new Vector2(x * uvFactorX, y * uvFactorY);
            }
        }

        index = 0;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                //第一个三角形（顺时针
                triangles[index] = (y * hCount2) + x;//左上角
                triangles[index + 2] = (y * hCount2) + x + 1;//右下角
                triangles[index + 1] = ((y + 1) * hCount2) + x;//左下角
                //第二个三角形（顺时针
                triangles[index + 3] = ((y + 1) * hCount2) + x;//左上角
                triangles[index + 4] = ((y + 1) * hCount2) + x + 1;//右上角
                triangles[index + 5] = (y * hCount2) + x + 1;//右下角
                index += 6;
            }
        }
        m.vertices = vertices;
        m.normals = normals;
        m.uv = uv;
        m.triangles = triangles;
        m.RecalculateBounds();

        return m;
    }
    public float GetDisplacement(float x, float y)
    {
        float perlinVal = Mathf.PerlinNoise(x * noiseScale, y * noiseScale);
        float perlinVal2 = Mathf.PerlinNoise(x * noiseScale * 1589175.42f, y * noiseScale * 5151.22f);
        perlinVal = Mathf.Lerp(perlinVal2, perlinVal, 0.5f);

        return perlinVal;
    }
}
