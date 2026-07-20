using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ArrowGenerator : MonoBehaviour
{

    public float tipLength;
    public float tipWidth;
    public float stemWidth;

    [System.NonSerialized] public List<Vector3> verticesList;
    [System.NonSerialized] public List<int> triangleList;
    
    private Mesh mesh;

    public void OnCreate()
    {
        mesh = new Mesh();
        this.GetComponent<MeshFilter>().mesh = mesh;
    }

    public void GenerateArrow(Vector3 pointA, Vector3 pointB)
    {
        verticesList = new List<Vector3>();
        triangleList = new List<int>();
        
        Vector3 stemOrigin = pointA;
        Vector3 arrowTerminator = pointB;
        float stemHalfWidth = stemWidth / 2;
        
        float arrowLength = Vector3.Distance(stemOrigin, arrowTerminator);
        float stemLength = arrowLength - tipLength;
        Vector3 direction = arrowTerminator - stemOrigin;
        Vector3 normalizedDir = Vector3.Normalize(new Vector3(direction.x, direction.y, 0));
        Vector3 stemTerminator = stemOrigin + (normalizedDir * stemLength); 
        
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
        
        //Tip Setup
        float tipHalfWidth = tipWidth / 2;

        Vector3 tipOffset = perpendicular * tipHalfWidth;
        
        // tip points
        verticesList.Add(stemTerminator + tipOffset);
        verticesList.Add(stemTerminator - tipOffset);
        verticesList.Add(arrowTerminator);
        
        // tip triangle
        triangleList.Add(4);
        triangleList.Add(6);
        triangleList.Add(5);

        mesh.vertices = verticesList.ToArray();
        mesh.triangles = triangleList.ToArray();
    }
}
