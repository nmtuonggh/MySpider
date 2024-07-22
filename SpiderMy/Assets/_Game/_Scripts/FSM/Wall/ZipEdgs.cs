using System.Collections.Generic;
using UnityEngine;

namespace SFRemastered.Wall
{
    public class ZipEdgs : MonoBehaviour
    {
        public List<ZipPoint> Points;

        public Vector3 GetClosestPointOnEdge(Vector3 point)
        {
            float minDistance = float.MaxValue;
            Vector3 closestPoint = Vector3.zero;

            for (int i = 0; i < Points.Count - 1; i++)
            {
                Vector3 A = Points[i].PointTransform.position;
                Vector3 B = Points[i + 1].PointTransform.position;
                Vector3 AP = point - A;
                Vector3 AB = B - A;
                float magnitudeAB = AB.sqrMagnitude;
                float ABAPproduct = Vector3.Dot(AP, AB);
                float distance = ABAPproduct / magnitudeAB;

                Vector3 pointOnLine;
                if (distance < 0)
                {
                    pointOnLine = A;
                }
                else if (distance > 1)
                {
                    pointOnLine = B;
                }
                else
                {
                    pointOnLine = A + AB * distance;
                }

                float pointDistance = Vector3.Distance(pointOnLine, point);
                if (pointDistance < minDistance)
                {
                    minDistance = pointDistance;
                    closestPoint = pointOnLine;
                }
            }

            return closestPoint;
        }
    }
}