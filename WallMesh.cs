using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMesh : MonoBehaviour
{
    // Start is called before the first frame update
    Mesh mesh;
    List<Vector3> vertices;
    List<int> triangles;
     float scale = 2f;
    float adjustedScale;
    Vector3[] meshDataVertices;
    int[][] meshDataTriangles =
   {
        new int[]{0,1,2,3},
        new int[]{5,0,3,6},
        new int[]{4,5,6,7},
        new int[]{1,4,7,2},
        new int[]{5,4,1,0},
        new int[]{3,2,7,6},


    };
    public void  setPoints(float x1,float y1,float x2,float y2)
    {
        x1 = x1 * scale;
        x2 = x2 * scale;
        y1 = y1 * scale;
        y2 = y2 * scale;
        meshDataVertices = new Vector3[]{
        new Vector3(y1, 1, x2),
        new Vector3(y1, 1, x1),
        new Vector3(y1, 0, x1),
        new Vector3(y1, 0, x2),
        new Vector3(y2, 1, x1),
        new Vector3(y2, 1, x2),
        new Vector3(y2, 0, x2),
        new Vector3(y2, 0, x1)
     };

    }
   
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        adjustedScale = scale * 5f;
        MakeCube();
        updateMesh();
        MeshRenderer mr = GetComponent<MeshRenderer>();
        mr.material = new Material(Shader.Find("Diffuse"));

    }
    public void MakeCube()
    {
        vertices = new List<Vector3>();
        triangles = new List<int>();
        for (int i = 0; i < 6; i++)
        {
            MakeFace(i, adjustedScale);
        }
    }
    void MakeFace(int dir, float faceScale)
    {
        vertices.AddRange(faceVertices(dir, faceScale));
        triangles.Add(vertices.Count - 4);
        triangles.Add(vertices.Count - 4 + 1);
        triangles.Add(vertices.Count - 4 + 2);
        triangles.Add(vertices.Count - 4);
        triangles.Add(vertices.Count - 4 + 2);
        triangles.Add(vertices.Count - 4 + 3);
    }
    public  Vector3[] faceVertices(int dir, float scale)
    {
        Vector3[] fv = new Vector3[4];
        for (int i = 0; i < fv.Length; i++)
        {
            fv[i] = meshDataVertices[meshDataTriangles[dir][i]] ;

        }
        return fv;
    }
    void updateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();
    }
    // Update is called once per frame

}
