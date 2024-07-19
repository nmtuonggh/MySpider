using System;
using UnityEngine;

namespace SFRemastered
{
    public class FindTargetZipOnPlane : MonoBehaviour
    {
        public PlaneEdges planeEdges;
        public LayerMask layerMask;
        public new Camera camera;
        private void Update()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, Mathf.Infinity, layerMask);
            if (hasHit)
            {
                planeEdges = hit.collider.GetComponent<PlaneEdges>();
                Vector3 intersectionPoint = hit.point;
                
                var closestEdge = FindClosestEdge(intersectionPoint);
                var closestPointOnEdge = ClosestPointOnLine(closestEdge.Item1, closestEdge.Item2, intersectionPoint);
                
                Debug.DrawLine(intersectionPoint, closestPointOnEdge, Color.cyan);
            }
        }
        
        (Vector3, Vector3) FindClosestEdge(Vector3 point)
        {
            var edges = planeEdges.GetEdges();
            float minDistance = float.MaxValue;
            (Vector3, Vector3) closestEdge = (Vector3.zero, Vector3.zero);

            foreach (var edge in edges)
            {
                float distance = Vector3.Distance(ClosestPointOnLine(edge.Item1, edge.Item2, point), point);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestEdge = edge;
                }
            }

            return closestEdge;
        }
        
        Vector3 ClosestPointOnLine(Vector3 A, Vector3 B, Vector3 P)
        {
            Vector3 AP = P - A;
            Vector3 AB = B - A;
            float magnitudeAB = AB.sqrMagnitude;
            float ABAPproduct = Vector3.Dot(AP, AB);
            float distance = ABAPproduct / magnitudeAB;

            if (distance < 0)
            {
                return A;
            }
            else if (distance > 1)
            {
                return B;
            }
            else
            {
                return A + AB * distance;
            }
        }
    }
}