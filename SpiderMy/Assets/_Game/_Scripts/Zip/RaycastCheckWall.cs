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
        public float inwardsOffset = 0.1f;
        public Vector3 pointSetBack;
        public Vector3 zipPoint;
        public GameObject focusZipPointPrefab;
        public RectTransform focusPrefabRectTransform;

        private void Update()
        {
            if (Physics.Raycast(camera.transform.position, camera.transform.forward, out RaycastHit hit, distance,
                    layerMask))
            {
                hitSurface = hit;
                pointSetBack = hit.point - hit.normal * inwardsOffset; 
                //check if vector hit.normal not direction with vector3.up
                if (Vector3.Dot(hit.normal, Vector3.up) <= 0.99f)
                {
                    var direc = Vector3.ProjectOnPlane(Vector3.up, hit.normal);
                    if (Physics.Raycast(pointSetBack, direc, out RaycastHit hit2, distance,
                            layerMask))
                    {
                        zipPoint = hit2.point;
                        ShowFocusZipPoint();
                    }
                }
                else
                {
                    
                }
            }else
            {
                zipPoint = Vector3.zero;
                if (focusPrefabRectTransform.gameObject.activeSelf)
                {
                    focusPrefabRectTransform.gameObject.SetActive(false);
                }
            }
        }
        
        private void ShowFocusZipPoint()
        {
            Vector3 screenPoint = camera.WorldToScreenPoint(zipPoint);
            if (focusPrefabRectTransform != null)
            {
                focusPrefabRectTransform.position = screenPoint;
                if (focusPrefabRectTransform.gameObject.activeSelf == false)
                {
                    focusPrefabRectTransform.gameObject.SetActive(true);
                }
            }
        }
        
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(pointSetBack, Vector3.ProjectOnPlane(Vector3.up, hitSurface.normal) * distance);
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(pointSetBack, 0.5f);
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(zipPoint, 0.5f);
            //Gizmos.DrawWireSphere(zipPoint, 0.1f);
        }
    }
}