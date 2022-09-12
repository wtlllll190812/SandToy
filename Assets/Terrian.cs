using System;
using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;
using System.Collections.Generic;

public class Terrian : MonoBehaviour
{
    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;

    //两点之间的宽度控制
    public float width = 0.1f;
    //高度控制
    public int height = 6;
    //缩放系数
    public float noiseParam = 0.1f;
    //偏移量
    public float offsetX;
    public float offsetZ;


    //每行/列元素数目
    public int N = 10;
    //顶点数组
    public List<Vector3> vertices;
    //三角形数组
    public List<int> triangles;

    void Start()
    {
        vertices = new List<Vector3>();
        triangles = new List<int>();
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = new MeshRenderer();
        GenerateNoise();
    }

    [Button("GenMesh")]
    public void GenMesh()
    {
        //构造网格
        Mesh mesh = new Mesh();
        //构造顶点及其三角面
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        //重新计算所有的边和法线
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        meshFilter.mesh = mesh;
    }

    public void GenerateNoise()
    {
        ClearMeshData();

        //生成顶点
        for (int z = 0; z < N; z++)
            for (int x = 0; x < N; x++)
            {
                int index = (N * z + x) * 4;
                float h = Mathf.Round(Mathf.PerlinNoise(x * noiseParam + offsetX, z * noiseParam + offsetZ) * height);
                vertices.Add(new Vector3(x-0.5f, h, z+0.5f));
                vertices.Add(new Vector3(x+0.5f, h, z+0.5f));
                vertices.Add(new Vector3(x+0.5f, h, z-0.5f));
                vertices.Add(new Vector3(x-0.5f, h, z-0.5f));

                int indexLeft = x > 0 ? (N * z + x - 1) * 4 : -1;
                int indexDown = z > 0 ? (N * (z - 1) + x) * 4 : -1;
                if (indexLeft != -1 && Vector3.Distance(vertices[indexLeft + 1], vertices[index]) > 0.1f)
                {
                    triangles.Add(indexLeft + 1);
                    triangles.Add(index);
                    triangles.Add(indexLeft + 2);
                    triangles.Add(index);
                    triangles.Add(index + 3);
                    triangles.Add(indexLeft + 2);
                }
                if (indexDown != -1 && Vector3.Distance(vertices[indexDown], vertices[index + 3]) > 0.1f)
                {
                    triangles.Add(index + 2);
                    triangles.Add(indexDown + 1);
                    triangles.Add(indexDown);
                    triangles.Add(indexDown);
                    triangles.Add(index+3);
                    triangles.Add(index + 2);
                }

                triangles.Add(index);
                triangles.Add(index + 1);
                triangles.Add(index + 2);
                triangles.Add(index + 2);
                triangles.Add(index + 3);
                triangles.Add(index + 0);
            }
        
        //构造网格
        Mesh mesh = new Mesh();
        //构造顶点及其三角面
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        //重新计算所有的边和法线
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        meshFilter.mesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;
    }

    public void ClearMeshData()
    {
        vertices.Clear();
        triangles.Clear();
    }
}
