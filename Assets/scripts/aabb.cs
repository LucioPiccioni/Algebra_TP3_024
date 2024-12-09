using System;
using System.Collections.Generic;
using UnityEngine;

public class aabb : MonoBehaviour
{

    [SerializeField] private Vector3 origin;
    [SerializeField] private Vector3 size;


    private MeshFilter meshFilter;
    private Vector3[] vertices;


    private Vector3 minV;
    private Vector3 maxV;

    private void Start()
    {
        meshFilter = GetComponentInChildren<MeshFilter>();
        vertices = meshFilter.mesh.vertices;

        UpdateBounds();
    }

    private void Update()
    {
        UpdateBounds();
    }


    private void UpdateBounds()
    {
        minV = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
        maxV = new Vector3(float.MinValue, float.MinValue, float.MinValue);


        foreach (Vector3 vertex in vertices)
        {
            Vector3 globalVertex = transform.TransformPoint(vertex);
            minV = Vector3.Min(minV, globalVertex);
            maxV = Vector3.Max(maxV, globalVertex);
        }
    }

    public bool IsColliding(aabb other)
    {
        float aMaxX = GetCenter().x + GetSize().x / 2;
        float aMaxY = GetCenter().y + GetSize().y / 2;
        float aMaxZ = GetCenter().z + GetSize().z / 2;

        float aMinX = GetCenter().x - GetSize().x / 2;
        float aMinY = GetCenter().y - GetSize().y / 2;
        float aMinZ = GetCenter().z - GetSize().z / 2;

        float bMaxX = other.GetCenter().x + other.GetSize().x / 2;
        float bMaxY = other.GetCenter().y + other.GetSize().y / 2;
        float bMaxZ = other.GetCenter().z + other.GetSize().z / 2;

        float bMinX = other.GetCenter().x - other.GetSize().x / 2;
        float bMinY = other.GetCenter().y - other.GetSize().y / 2;
        float bMinZ = other.GetCenter().z - other.GetSize().z / 2;

        if (aMinX <= bMaxX && aMaxX >= bMinX &&
                aMinY <= bMaxY && aMaxY >= bMinY &&
                aMinZ <= bMaxZ && aMaxZ >= bMinZ)
        {
            return true;
        }

        return false;
    }

    public Vector3 GetCenter()
    {
        return (minV + maxV) / 2;
    }

    public Vector3 GetSize()
    {
        return maxV - minV;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(GetCenter(), GetSize());
    }
}