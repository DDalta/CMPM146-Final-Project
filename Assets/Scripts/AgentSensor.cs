using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AgentSensor : MonoBehaviour
{
    public float distance = 10;
    public float angle = 30;
    public Color meshColor = Color.red;

    public int scanFrequency = 30;
    public LayerMask layers;
    public LayerMask occlusionLayers;
    public List<GameObject> Objects = new List<GameObject>();

    Collider2D[] colliders = new Collider2D[50];
    int count;
    float scanInterval;
    float scanTimer;

    Mesh mesh;

    // Start is called before the first frame update
    void Start()
    {
        scanInterval = 1.0f / scanFrequency;
    }

    // Update is called once per frame
    void Update()
    {
        // do not scan every frame instead do it every 1/30th of a second
        scanTimer -= Time.deltaTime;
        if (scanTimer < 0)
        {
            scanTimer += scanInterval;
            Scan();

        }
    }

    private void Scan()
    {
        // get every object colliding within the defined distance
        // make sure object is part of the specified layers
        count = Physics2D.OverlapCircleNonAlloc(transform.position, distance, colliders, layers, 0f, 0f);

        // clean up list before updating 
        Objects.Clear();
        // go through all objects within the disance and check if it is in the sight of the agent
        for(int i = 0; i < count; ++i)
        {
            GameObject obj = colliders[i].gameObject;
            if (IsInSight(obj))
            {
                Objects.Add(obj);
            }
        }
    }
    // check if an object is within the sight of the agent
    // return false if object not in the angle of the agent's sight
    // return false if object is behind a wall (walls can be defined in the occlusionLayers mask)
    public bool IsInSight(GameObject obj)
    {
        Vector3 origin = transform.position;
        Vector3 dest = obj.transform.position;
        Vector3 direction = dest - origin;

        float deltaAngle = Vector3.Angle(direction, transform.right);
        if (deltaAngle > angle)
        {
            return false;
        }

        if (Physics2D.Linecast(origin, dest, occlusionLayers))
        {
            return false;
        }

        return true;
    }

    // creates the mesh for Gizmos
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

    // updated when a value is changed in the inspector
    private void OnValidate()
    {
        mesh = CreateWedgeMesh();
        scanInterval = 1.0f / scanFrequency;
    }

    // draw all gizmos for debugging
    private void OnDrawGizmos()
    {
        // agent's sight mesh
        if (mesh)
        {
            Gizmos.color = meshColor;
            Gizmos.DrawMesh(mesh, transform.position, transform.rotation);
        }

        // draw red sphere for objects within defined distance
        Gizmos.DrawWireSphere(transform.position, distance);
        for(int i = 0; i < count; ++i) 
        {
            Gizmos.DrawSphere(colliders[i].transform.position, 0.2f);
        }


        // draw greeen sphere for objects in agent's line of sight
        Gizmos.color = Color.green;
        foreach (var obj in Objects)
        {
            Gizmos.DrawSphere(obj.transform.position, 0.2f);
        }
    }
}
