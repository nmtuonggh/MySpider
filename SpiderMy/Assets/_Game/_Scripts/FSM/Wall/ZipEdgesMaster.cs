using System.Collections.Generic;
using UnityEngine;

namespace SFRemastered.Wall
{
    public class ZipEdgesMaster : MonoBehaviour
    {
        public List<ZipEdgs> ZipEdges;

        public Vector3 GetClosestPointOnAnyEdge(Vector3 point)
        {
            float minDistance = float.MaxValue;
            Vector3 closestPoint = Vector3.zero;

            foreach (var edge in ZipEdges)
            {
                Vector3 pointOnEdge = edge.GetClosestPointOnEdge(point);
                float distance = Vector3.Distance(pointOnEdge, point);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestPoint = pointOnEdge;
                }
            }

            return closestPoint;
        }
    }
}