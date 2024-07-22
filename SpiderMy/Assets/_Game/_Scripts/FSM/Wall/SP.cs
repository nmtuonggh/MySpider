using SFRemastered.Wall;
using UnityEngine;
using UnityEngine.Serialization;

namespace SFRemastered
{
    public class SP : MonoBehaviour
    {
        public ZipEdgesMaster ZipEdgesMaster;
        public new Camera  camera;
        public LayerMask LayerMask;
        public float SphereCastRadius;

        private void Update()
        {
            
            RaycastHit hit;
            var hasHit = Physics.SphereCast(camera.transform.position, SphereCastRadius, camera.transform.forward, out hit, Mathf.Infinity, LayerMask);
            if (hasHit)
            {
                ZipEdgesMaster = hit.collider.GetComponentInChildren<ZipEdgesMaster>();
                Vector3 intersectionPoint = hit.point;
                Vector3 closestPointOnEdge = ZipEdgesMaster.GetClosestPointOnAnyEdge(intersectionPoint);

                Debug.DrawLine(intersectionPoint, closestPointOnEdge, Color.cyan);
            }
        }
    }
}