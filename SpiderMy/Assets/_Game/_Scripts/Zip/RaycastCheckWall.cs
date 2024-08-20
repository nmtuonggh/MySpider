using System;
using Animancer;
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
        
        public Vector3 zipPoint;
        public Vector3 pointSetBack;
        public Vector3 upPoint;
        
        public float cameraSphereRadius = 0.5f;
        public float upPointSphereRadius = 0.5f;
        public float upSphereCastHeight = 100f;
        
        public GameObject focusZipPointPrefab;
        public RectTransform focusPrefabRectTransform;

        private void Update()
        {
            if (Physics.SphereCast(camera.transform.position, cameraSphereRadius, camera.transform.forward, out RaycastHit hit, distance,
                    layerMask))
            {
                hitSurface = hit;
                pointSetBack = hit.point - hit.normal * inwardsOffset; 
                if (Vector3.Dot(hit.normal, Vector3.up) <= 0.99f)
                {
                    var direc = Vector3.ProjectOnPlane(Vector3.up, hit.normal);
                    if (Physics.Raycast(pointSetBack, direc, out RaycastHit hit2, distance, layerMask))
                    {
                        upPoint = hit2.point + Vector3.up * upSphereCastHeight;
                        Debug.DrawRay(upPoint, -hit2.transform.up, Color.blue);
                        if (Physics.SphereCast(upPoint, upPointSphereRadius, -hit2.transform.up, out RaycastHit hit3, upSphereCastHeight,
                            layerMask))
                        {
                            Debug.DrawRay( hit3.point, hit3.normal, Color.magenta);
                            if (Vector3.Angle(hit3.normal, Vector3.up)<45)
                            {
                                Debug.Log("Angle");
                                zipPoint = hit3.point;
                                ShowFocusZipPoint();
                            }
                            else
                            {
                                Debug.Log("Null");
                            }
                        }
                        else
                        {
                            Debug.Log("Hit 3 null");
                        }
                    }
                }
                else
                {
                    Debug.Log("Hit 2 null");
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
            /*Gizmos.color = Color.red;
            Gizmos.DrawRay(pointSetBack, Vector3.ProjectOnPlane(Vector3.up, hitSurface.normal) * distance);*/
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(pointSetBack, 0.5f);
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(upPoint, Vector3.down * upSphereCastHeight);
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(zipPoint, 0.5f);
        }
    }
}