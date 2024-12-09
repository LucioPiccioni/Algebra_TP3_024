using System;
using System.Collections.Generic;
using System.Linq;
using Vector3 = UnityEngine.Vector3;
using UnityEngine;

public class NormalsAndMesh : MonoBehaviour
{
    [SerializeField] private List<Poligon> poligon = new List<Poligon>();
    [SerializeField] private MeshFilter meshFilter;
    [SerializeField] private List<Plane> planes = new List<Plane>();

    private void Awake()
    {
        meshFilter = GetComponentInChildren<MeshFilter>();

        if (meshFilter == null) return;

        Vector3[] vertices = meshFilter.mesh.vertices;
        int[] meshTriangles = meshFilter.mesh.triangles;


        for (int i = 0; i < meshTriangles.Length; i += 3)
        {
            Poligon poligon = new Poligon();

            poligon.SetVertices(
                vertices[meshTriangles[i]],
                vertices[meshTriangles[i + 1]],
                vertices[meshTriangles[i + 2]]
            );

            this.poligon.Add(poligon);


            planes.Add(new Plane(poligon.vertices[0], poligon.vertices[1], poligon.vertices[2]));
        }
    }

    private void OnDrawGizmos()
    {
        planes.ForEach(plane => plane.DrawGizmo(transform));
    }

    public bool WorkingContainAPoint(Vector3 point)
    {
        Vector3 localPoint = transform.InverseTransformPoint(point);

        //verifica el punto si esta dentro de cada plano
        foreach (Plane plane in planes)
        {
            //si el punto esta en negativo (lado negativo) de cualquier plano, este esta afuera
            if (!plane.IsNegativeToThePlane(localPoint))
            {
                return false;
            }
        }

        //Si esta dentro de todos entonces lo contiene
        return true;
    }
}
