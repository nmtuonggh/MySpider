using System;
using Animancer;
using UnityEngine;

namespace SFRemastered._Game._Scripts.Zip
{
    public class RaycastCheckWall : MonoBehaviour
    {
        public new Camera cam;
        public LayerMask layerMask;
        public LayerMask GroundLayer;
        public LayerMask WallLayer;
        public RaycastHit hitSurface;

        public Vector3 zipPoint;
        public Vector3 pointSetBack;
        public Vector3 upPoint;

        public float zipDetectionRange = 0.5f;
        public float zipDetectionLength = 35f;
        public RaycastHit zipPointDetection;

        public float inwardsOffset = 0.1f;
        public float upPointSphereRadiusFace = 6f;
        public float upSphereCastHeightFace = 50f;
        public float upPointSphereRadiusTop = 0.6f;
        public float upSphereCastHeightTop = 20f;

        public float forwardPointDistance = 10f;

        //public GameObject focusZipPointPrefab;
        public RectTransform focusPrefabRectTransform;

        private RaycastHit hit2;

        private void Update()
        {
            if (Physics.SphereCast(cam.transform.position, zipDetectionRange, cam.transform.forward, out RaycastHit hit,
                    zipDetectionLength, layerMask))
            {
                if (hit.transform.gameObject.layer == GroundLayer)
                {
                    Debug.Log("run");
                    zipPoint = Vector3.zero;
                    if (focusPrefabRectTransform.gameObject.activeSelf)
                    {
                        focusPrefabRectTransform.gameObject.SetActive(false);
                    }
                    return;
                }

                hitSurface = hit;
                pointSetBack = hit.point - hit.normal * inwardsOffset;
                if (Vector3.Dot(hit.normal, Vector3.up) <= 0.99f)
                {
                    FaceZipPoint();
                }
                else
                {
                    FaceDownZipPoint();
                }
            }
            else
            {
                zipPoint = Vector3.zero;
                if (focusPrefabRectTransform.gameObject.activeSelf)
                {
                    focusPrefabRectTransform.gameObject.SetActive(false);
                }
            }
            
            if (zipPoint == Vector3.zero)
            {
                focusPrefabRectTransform.gameObject.SetActive(false);
            }
        }

        private void FaceZipPoint()
        {
            upPoint = pointSetBack + Vector3.up * upSphereCastHeightFace;
            if (Physics.SphereCast(upPoint, upPointSphereRadiusFace, Vector3.down, out RaycastHit hit3,
                    upSphereCastHeightFace, WallLayer))
            {
                if (Vector3.Angle(hit3.normal, Vector3.up) < 45)
                {
                    zipPoint = hit3.point;
                    ShowFocusZipPoint();
                }
                else
                {
                    zipPoint = Vector3.zero;
                    if (focusPrefabRectTransform.gameObject.activeSelf)
                    {
                        focusPrefabRectTransform.gameObject.SetActive(false);
                    }
                }
            }
            else
            {
                zipPoint = Vector3.zero;
                if (focusPrefabRectTransform.gameObject.activeSelf)
                {
                    focusPrefabRectTransform.gameObject.SetActive(false);
                }
            }
        }

        private void FaceDownZipPoint()
        {
            var direc = Vector3.ProjectOnPlane(cam.transform.forward, hitSurface.normal);
            var direc1 = Vector3.ProjectOnPlane(-cam.transform.forward, hitSurface.normal);
            Vector3 forwardPoint = pointSetBack + direc.normalized * forwardPointDistance;
            Vector3 forwardPoint1 = pointSetBack + direc1.normalized * forwardPointDistance;

            #region Tutruocvao

            if (Physics.Raycast(forwardPoint, -direc, out hit2, upSphereCastHeightTop, WallLayer))
            {
                Vector3 upPoint = hit2.point + Vector3.up * upSphereCastHeightTop;
                Vector3 upPoint2 = this.transform.position + Vector3.up * upSphereCastHeightTop;

                if (Physics.SphereCast(upPoint, upPointSphereRadiusTop, Vector3.down, out RaycastHit hit3,
                        zipDetectionLength, WallLayer)
                    && !Physics.Raycast(upPoint2, direc, zipDetectionLength, WallLayer))
                {
                    zipPoint = hit3.point;
                    ShowFocusZipPoint();
                }
                else
                {
                    zipPoint = Vector3.zero;
                    if (focusPrefabRectTransform.gameObject.activeSelf)
                    {
                        focusPrefabRectTransform.gameObject.SetActive(false);
                    }
                }
            }
            else if (Physics.Raycast(forwardPoint1, -direc1, out hit2, upSphereCastHeightTop, WallLayer))
            {
                Vector3 upPoint = hit2.point + Vector3.up * upSphereCastHeightTop;
                Vector3 upPoint2 = this.transform.position + Vector3.up * upSphereCastHeightTop;

                if (Physics.SphereCast(upPoint, upPointSphereRadiusTop, Vector3.down, out RaycastHit hit3,
                        zipDetectionLength, WallLayer)
                    && !Physics.Raycast(upPoint2, direc, zipDetectionLength, WallLayer))
                {
                    zipPoint = hit3.point;
                    ShowFocusZipPoint();
                }
                else
                {
                    zipPoint = Vector3.zero;
                    if (focusPrefabRectTransform.gameObject.activeSelf)
                    {
                        focusPrefabRectTransform.gameObject.SetActive(false);
                    }
                }
            }
            else
            {
                //Debug.Log("hit2 - FaceDownZipPoint null");
                zipPoint = Vector3.zero;
                if (focusPrefabRectTransform.gameObject.activeSelf)
                {
                    focusPrefabRectTransform.gameObject.SetActive(false);
                }
            }

            #endregion
        }

        private void ShowFocusZipPoint()
        {
            Vector3 screenPoint = cam.WorldToScreenPoint(zipPoint);
            if (focusPrefabRectTransform != null)
            {
                focusPrefabRectTransform.position = screenPoint;
                if (focusPrefabRectTransform.gameObject.activeSelf == false)
                {
                    focusPrefabRectTransform.gameObject.SetActive(true);
                }
            }
        }


        /*private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(pointSetBack, 0.5f);
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(upPoint, Vector3.down * upSphereCastHeight);
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(zipPoint, 0.5f);
        }*/
    }
}