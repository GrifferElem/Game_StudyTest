using UnityEngine;

public class ProgramMesh : MonoBehaviour
{
    public int width = 10, height = 10;
    public float noiseScale = 10;
    public int grassCount = 1000;//number of grass objects to create
    public float grassScale = 0.1f;//size of the grass object

    public Material grassMaterial;

    private void Start()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        Mesh terrianMesh = GeneratePlane();
        meshFilter.mesh = terrianMesh;

        GenerateGrass(terrianMesh);
    }

    private void GenerateGrass(Mesh terrianMesh)
    {
        for(int i = 0; i < grassCount; i++)
        {
            // Pick a random vertex on the terrain to place the grass
            float vx = Random.Range(0, width);
            float vz = Random.Range(0, height);
            float vy = GetDisplacement(vx, vz);
            Vector3 vertex = new Vector3(vx-width / 2f, vy,vz-height/2f);

            //Create a new grass object
            GameObject grass = new GameObject("Grass");
            MeshFilter grassMeshFilter = grass.AddComponent<MeshFilter>();
            MeshRenderer grassMeshRenderer = grass.AddComponent<MeshRenderer>();
            grassMeshRenderer.material = grassMaterial;
            grassMeshFilter.mesh = GenerateGrassMesh();

            //Position the grass object at the selected vertex, taking into account the terrain's position
            grass.transform.position = vertex;//TransformPoint converts local to world coordinates
            //Adjust the scale of the grass object
            grass.transform.localScale = new Vector3(grassScale,grassScale, grassScale);
            //Make the grass object a child of the terrain object
            grass.transform.SetParent(this.transform, true);
        }
    }

    private Mesh GenerateGrassMesh()
    {
        Mesh m = new Mesh();
        m.name = "GrassMesh";

        // Define vertices for a simple 3-blade, 3-segment grass mesh
        Vector3[] vertices = new Vector3[]
        {
            new Vector3(-0.5f, 0, 0), // Blade 1 segment 1 bottom
            new Vector3(-0.25f, 1, 0), // Blade 1 segment 1 top / segment 2 bottom
            new Vector3(-0.15f, 1.2f, 0), // Blade 1 segment 2 top / segment 3 bottom
            new Vector3(0, 1.5f, 0), // Blade 1 segment 3 top
        };

        //Define triangles for the 3 blades
        int[] triangles = new int[] {
            0, 1, 2, // Blade 1 segment 1
            1, 2, 3, // Blade 1 segment 2
            2, 3, 0, // Blade 1 segment 3
        };
        //Define UV coordinates
        Vector2[] uv = new Vector2[vertices.Length];
        for(int i = 0; i < uv.Length; i++)
        {
            uv[i] = new Vector2(vertices[i].x, vertices[i].y);
        }

        m.vertices = vertices;
        m.triangles = triangles;
        m.uv = uv;

        m.RecalculateNormals();
        m.RecalculateBounds();

        return m;
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
        float scaleX = 1;
        float scaleY = 1;
        for(float y = 0; y < vCount2; y++)
        {
            for(float x = 0; x < hCount2; x++)
            {
                vertices[index] = new Vector3(x * scaleX - width / 2f, 0, y * scaleY - height / 2f)+Vector3.up*GetDisplacement(x,y)*scaleY;
                normals[index] = Vector3.up;
                uv[index++] = new Vector2(x * uvFactorX,y * uvFactorY);
            }
        }

        index = 0;
        for(int y = 0; y < height; y++)
        {
            for(int x = 0; x < width; x++)
            {
                //第一个三角形（顺时针
                triangles[index] = (y * hCount2) + x;//左上角
                triangles[index + 1] = ((y + 1) * hCount2) + x;//左下角
                triangles[index + 2] = (y * hCount2) + x + 1;//右下角
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
    public float GetDisplacement(float x,float y)
    {
        float perlinVal = Mathf.PerlinNoise(x*noiseScale,y*noiseScale);
        float perlinVal2 = Mathf.PerlinNoise(x*noiseScale* 1589175.42f, y * noiseScale * 5151.22f);
        perlinVal = Mathf.Lerp(perlinVal2, perlinVal, 0.5f);

        perlinVal *= 2;
        perlinVal = Mathf.Pow(perlinVal, 3);
        return perlinVal;
    }
}
