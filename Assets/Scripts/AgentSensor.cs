using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentSensor : MonoBehaviour
{
    public float distance = 10;
    public float angle = 30;
    public Color meshColor = Color.red;

    Mesh mesh;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Mesh CreateWedgeMesh()
    {
        Mesh mesh = new Mesh();

        int segments = 10;
        int numTriangles = segments * 4;
        int numVertices = numTriangles * 3;

        Vector3[] vertices = new Vector3[numVertices];
        int[] triganles = new int[numVertices];

        Vector3 center = Vector3.zero;
        Vector3 left = Quaternion.Euler(0f, 0f, angle) * Vector3.right * distance;
        Vector3 right = Quaternion.Euler(0f, 0f, -angle) * Vector3.right * distance;

        int vert = 0;

        float currentAngle = -angle;
        float deltaAngle = (angle * 2) / segments;

        for(int i = 0; i < segments; ++i)
        {
            left = Quaternion.Euler(180f, 0f, currentAngle) * Vector3.right * distance;
            right = Quaternion.Euler(180f, 0f, currentAngle + deltaAngle) * Vector3.right * distance;

            vertices[vert++] = center;
            vertices[vert++] = left;
            vertices[vert++] = right;

            currentAngle += deltaAngle;
        }

        for (int i = 0; i < numVertices; ++i)
        {
            triganles[i] = i;
        }

        mesh.vertices = vertices;
        mesh.triangles = triganles;
        mesh.RecalculateNormals();

        return mesh;
    }

    private void OnValidate()
    {
        mesh = CreateWedgeMesh();
    }

    private void OnDrawGizmos()
    {
        if (mesh)
        {
            Gizmos.color = meshColor;
            Gizmos.DrawMesh(mesh, transform.position, transform.rotation);
        }
    }
}
