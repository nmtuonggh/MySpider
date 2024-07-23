using System;
using UnityEngine;

namespace SFRemastered._Game._Scripts.CastCheck.Raycast
{
    public class FindZipPoint : MonoBehaviour
    {
        public new Camera camera;
        public GameObject focusZipPointPrefab;
        public LayerMask wallLayer;

        public Vector3 zipPoint;
        public RaycastHit SphereCastDetected;

        public float spcastRadius;
        public float spcastDistance;

        private RectTransform focusPrefabRectTransform;


        private void Start()
        {
            focusPrefabRectTransform = focusZipPointPrefab.GetComponent<RectTransform>();
            focusZipPointPrefab.SetActive(false);
        }

        private void Update()
        {
            DetectZipPoint();
        }

        private void DetectZipPoint()
        {
            if (Physics.SphereCast(camera.transform.position, spcastRadius, camera.transform.forward,
                    out SphereCastDetected, spcastDistance, wallLayer))
            {
                var wallScript = SphereCastDetected.transform.GetComponent<Town>();
                zipPoint = wallScript.GetZipPoint(SphereCastDetected.point);
                ShowFocusZipPoint();
            }
            else if (focusZipPointPrefab.activeSelf)
            {
                focusZipPointPrefab.SetActive(false);
            }
        }

        private void ShowFocusZipPoint()
        {
            Vector3 screenPoint = camera.WorldToScreenPoint(zipPoint);
            if (focusPrefabRectTransform != null)
            {
                focusPrefabRectTransform.position = screenPoint;
                if (!focusZipPointPrefab.activeSelf)
                {
                    focusZipPointPrefab.SetActive(true);
                }
            }
        }

        /*private void OnDrawGizmos()
        {
            Gizmos.DrawSphere(SphereCastDetected.point, spcastRadius);
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(zipPoint, spcastRadius);
        }*/
    }
}