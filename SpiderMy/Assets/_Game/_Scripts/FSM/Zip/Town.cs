using System.Collections.Generic;
using UnityEngine;


public class Town : UnityEngine.MonoBehaviour
{
    public MeshCollider meshCollider;
    public List<Vector3> verticesList = new List<Vector3>();
    public Vector3[] verticesList2;
    

    private void Awake()
    {
        meshCollider = GetComponent<MeshCollider>();
    }

    private void Start()
    {
        GetVertices();
    }

    private void GetVertices()
    {
        Vector3[] vertices = meshCollider.sharedMesh.vertices;
        verticesList2 = meshCollider.sharedMesh.vertices;
        Debug.Log("Count " + meshCollider.sharedMesh.vertices.Length);
        for (int i = 0; i < vertices.Length; i++)
        {
            /*var vertexTransform = vertices[i] * this.transform.lossyScale.x + this.transform.position;
            var vertexTransform.Scale(this.transform.lossyScale);*/
            
            /*var vertexTransform = vertices[i] + this.transform.position;
            vertexTransform.Scale(this.transform.lossyScale);*/
            
            var vertexTransform = vertices[i];
            vertexTransform.Scale(this.transform.lossyScale);
            vertexTransform += this.transform.position;

            if (!verticesList.Contains(vertexTransform) && vertices[i].y > 0.1)
            {
                verticesList.Add(vertexTransform);
            }
        }
    }

    public Vector3 GetZipPoint(Vector3 point)
    {
        if (verticesList.Count == 1) return verticesList[0];
        if (verticesList.Count == 7) verticesList.Remove(verticesList[6]);

        Vector3 minPoint = Vector3.zero;
        float minDistance = float.MaxValue;

        for (int i = 0; i < verticesList.Count; i++)
        {
            Vector3 projectedPoint;

            if (i == verticesList.Count - 1)
            {
                Debug.DrawLine(verticesList[i], verticesList[0], Color.red);
                projectedPoint = ProjectPointOnLine(point, verticesList[i], verticesList[0]);
            }
            else
            {
                Debug.DrawLine(verticesList[i], verticesList[i+1], Color.red);
                projectedPoint = ProjectPointOnLine(point, verticesList[i], verticesList[i + 1]);
            }

            float disctance = (point - projectedPoint).sqrMagnitude;

            if (minDistance >= disctance)
            {
                minDistance = disctance;
                minPoint = projectedPoint;
            }
            
        }

        return minPoint;
    }

    public Vector3 ProjectPointOnLine(Vector3 p, Vector3 a, Vector3 b)
    {
        return Vector3.Project((p-a), (b - a)) + a;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
    
        foreach (var verticies in verticesList)
        {
            Gizmos.DrawSphere(verticies, 1);
        }
    }
}