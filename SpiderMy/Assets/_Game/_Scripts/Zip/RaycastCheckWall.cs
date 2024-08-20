using System;
using UnityEngine;

namespace SFRemastered._Game._Scripts.Zip
{
    public class RaycastCheckWall : MonoBehaviour
    {
        public new Camera camera;
        public float distance = 40f;
        public LayerMask layerMask;
        public RaycastHit hitSurface;
        public float inwardsOffset = 0.2f;
        public Vector3 pointSetBack;

        private void Update()
        {
            if (Physics.Raycast(camera.transform.position, camera.transform.forward, out RaycastHit hit, distance,
                    layerMask))
            {
                hitSurface = hit;
                pointSetBack = hit.point - hit.normal * inwardsOffset;
                
                Vector3 parallelDirection = Vector3.Cross(hit.normal, camera.transform.forward);
                
            }
        }
    }
}