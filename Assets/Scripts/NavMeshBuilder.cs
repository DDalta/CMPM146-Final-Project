using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NavMeshPlus.Components;

public class NavMeshBuilder : MonoBehaviour
{
    public NavMeshSurface Surface2D;

    private void Start()
    {
        Surface2D.BuildNavMeshAsync();
    }
    public void buildNavMesh()
    {
        Surface2D.UpdateNavMesh(Surface2D.navMeshData);
    }
}
