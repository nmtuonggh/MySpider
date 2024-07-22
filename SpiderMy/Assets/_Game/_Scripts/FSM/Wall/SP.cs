using SFRemastered.Wall;
using UnityEngine;
using UnityEngine.Serialization;

namespace SFRemastered
{
    public class SP : MonoBehaviour
    {
        public ZipEdgesMaster ZipEdgesMaster;
        public new Camera camera;
        public Transform spcPoint;
        public LayerMask LayerMask;
        public float SphereCastRadius;
        public float SphereCastDistance;

        private void Update()
        {
            int playerLayer = LayerMask.NameToLayer("Player");

            LayerMask &= ~(1 << playerLayer);
            RaycastHit hit;
            bool hasHit = Physics.SphereCast(spcPoint.transform.position, SphereCastRadius,
                spcPoint.transform.forward, out hit, SphereCastDistance, LayerMask);
            if (hasHit)
            {
                Debug.Log("hit" + hit.transform.position);
                ProcessHit(hit);
            }
            else
            {
                //Debug.Log("No direct hit");
                FindClosestEdgeWithoutDirectHit();
            }
        }


        private void ProcessHit(RaycastHit hit)
        {
            ZipEdgesMaster = hit.collider.GetComponentInChildren<ZipEdgesMaster>();
            if (ZipEdgesMaster != null)
            {
                Vector3 intersectionPoint = hit.point;
                Vector3 closestPointOnEdge = ZipEdgesMaster.GetClosestPointOnAnyEdge(intersectionPoint);

                Debug.DrawLine(intersectionPoint, closestPointOnEdge, Color.cyan);
            }
        }

        private void FindClosestEdgeWithoutDirectHit()
        {
            float closestDistance = float.MaxValue;
            Vector3 closestPoint = Vector3.zero;
            Vector3 sphereCastOrigin = spcPoint.transform.position;
            foreach (var zipEdgesMaster in FindObjectsOfType<ZipEdgesMaster>())
            {
                if (Vector3.Distance(sphereCastOrigin, zipEdgesMaster.transform.position) <= SphereCastRadius)
                {
                    Vector3 potentialClosestPoint = zipEdgesMaster.GetClosestPointOnAnyEdge(sphereCastOrigin);
                    float distance = Vector3.Distance(sphereCastOrigin, potentialClosestPoint);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestPoint = potentialClosestPoint;
                    }
                }
            }

            if (closestDistance < float.MaxValue)
            {
                Debug.DrawLine(sphereCastOrigin, closestPoint, Color.cyan);
            }
        }

        /*private void OnDrawGizmos()
        {
            if (camera == null) return;

            Vector3 start = spcPoint.transform.position;
            Vector3 direction = spcPoint.transform.forward * SphereCastDistance;
            Vector3 end = start + direction;

            // Draw the direction line
            Debug.DrawLine(start, end, Color.red);

            // Draw start and end spheres
            DrawSphereCast(start, SphereCastRadius);
            DrawSphereCast(end, SphereCastRadius);
        }

        private void DrawSphereCast(Vector3 center, float radius)
        {
            float theta = 0;
            float x = radius * Mathf.Cos(theta);
            float y = radius * Mathf.Sin(theta);
            Vector3 pos = center + new Vector3(x, y, 0);
            Vector3 newPos = pos;
            Vector3 lastPos = pos;
            for (theta = 0.1f; theta < Mathf.PI * 2; theta += 0.1f)
            {
                x = radius * Mathf.Cos(theta);
                y = radius * Mathf.Sin(theta);
                newPos = center + new Vector3(x, y, 0);
                Debug.DrawLine(lastPos, newPos, Color.blue);
                lastPos = newPos;
            }

            // Repeat for the other two planes
            lastPos = pos;
            for (theta = 0.1f; theta < Mathf.PI * 2; theta += 0.1f)
            {
                x = radius * Mathf.Cos(theta);
                y = radius * Mathf.Sin(theta);
                newPos = center + new Vector3(x, 0, y);
                Debug.DrawLine(lastPos, newPos, Color.blue);
                lastPos = newPos;
            }

            lastPos = pos;
            for (theta = 0.1f; theta < Mathf.PI * 2; theta += 0.1f)
            {
                x = radius * Mathf.Cos(theta);
                y = radius * Mathf.Sin(theta);
                newPos = center + new Vector3(0, x, y);
                Debug.DrawLine(lastPos, newPos, Color.blue);
                lastPos = newPos;
            }
        }*/
    }
}