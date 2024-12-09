using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Grid3d : MonoBehaviour
{
    public GameObject pointPrefab;
    public int gridSizeX = 10;
    public int gridSizeY = 10;
    public int gridSizeZ = 10;
    public float spacing = 5.0f;
    public List<GameObject> objects = new List<GameObject>();
    private List<Vector3> gridPoints = new List<Vector3>();

    void Start()
    {
        GenerateGrid();
    }

    private void Update()
    {
        for (int i = 0; i < objects.Count(); i++)
        {
            for (int j = 0; j < objects.Count(); j++)
            {
                if (i != j)
                    CheckCollisions(objects[i], objects[j]);
            }
        }
    }

    void GenerateGrid()
    {

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                for (int z = 0; z < gridSizeZ; z++)
                {

                    Vector3 position = new Vector3(x * spacing, y * spacing, z * spacing);
                    gridPoints.Add(position);

                    Instantiate(pointPrefab, position, Quaternion.identity);
                }
            }
        }
    }

    void CheckCollisions(GameObject obj1, GameObject obj2)
    {
        aabb obj1Collider = obj1.GetComponent<aabb>();
        aabb obj2Collider = obj2.GetComponent<aabb>();

        if (obj1Collider && obj2Collider)
        {
            if (obj1Collider.IsColliding(obj2Collider))
            {
                NormalsAndMesh obj1Mesh = obj1.GetComponent<NormalsAndMesh>();
                NormalsAndMesh obj2Mesh = obj2.GetComponent<NormalsAndMesh>();

                for (int i = 0; i < gridPoints.Count(); i++)
                {
                    if (obj1Mesh.WorkingContainAPoint(gridPoints[i]) && obj2Mesh.WorkingContainAPoint(gridPoints[i]))
                    {
                        Debug.Log($"{obj1.name} colisiona con {obj2.name}");
                        break;
                    }
                }
            }
        }
    }

}
