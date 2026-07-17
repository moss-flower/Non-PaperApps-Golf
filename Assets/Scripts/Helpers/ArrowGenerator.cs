using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;


[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ArrowGenerator : MonoBehaviour
{

    public float stemLength;
    public float stemWidth;

    [System.NonSerialized] public List<Vector3> verticesList;
    [System.NonSerialized] public List<int> triangleList;
    
    [SerializeField] Transform pointA;
    [SerializeField] Transform pointB;
    private Mesh mesh;
    
    void Start()
    {
        mesh = new Mesh();
        this.GetComponent<MeshFilter>().mesh = mesh;
    }

    private void Update()
    {
        GenerateArrow();
    }

    void GenerateArrow()
    {
        verticesList = new List<Vector3>();
        triangleList = new List<int>();
        
        Vector3 stemOrigin = pointA.position;
        Vector3 stemTerminator = pointB.position;
        float stemHalfWidth = stemWidth / 2;
        
        Vector3 direction = stemTerminator - stemOrigin;
        Vector3 normalizedDir = Vector3.Normalize(new Vector3(direction.x, direction.y, 0));
        Vector3 perpendicular = new Vector3(-normalizedDir.y, normalizedDir.x, 0);
        
        Vector3 offset = perpendicular * stemHalfWidth; 
        
        //Stem 
        verticesList.Add(stemOrigin + offset);
        verticesList.Add(stemOrigin - offset);
        verticesList.Add(stemTerminator + offset);
        verticesList.Add(stemTerminator - offset);
        
        //Stem triangles
        triangleList.Add(0);
        triangleList.Add(1);
        triangleList.Add(3);
        
        triangleList.Add(0);
        triangleList.Add(3);
        triangleList.Add(2);

        mesh.vertices = verticesList.ToArray();
        mesh.triangles = triangleList.ToArray();
    }
}
