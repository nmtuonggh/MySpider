using System;
using System.Collections.Generic;

using UnityEngine;

namespace SFRemastered
{
    public class FindTargetZipPoint : MonoBehaviour
    {
        [SerializeField] private new Camera camera;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private float sphereRadius = 1f;
        [SerializeField] private GameObject zipPointFocusPrefab;
        [SerializeField] private int poolSize = 10;
        [SerializeField] private Transform poolSizeHolder;
        private List<GameObject> pool;
        public RaycastHit hitInfo;
        public GameObject lastInstantiatedObject;
        
        private void Awake()
        {
            InitializePool();
        }
        private void Update()
        {
            
            RaycastHit hit;
            bool hasHit = Physics.SphereCast(camera.transform.position, sphereRadius, camera.transform.forward, out hit, Mathf.Infinity, layerMask);

            // If a new point is detected and it's different from the last hit point or if there's no last hit point
            if (hasHit && (lastInstantiatedObject == null || hit.point != hitInfo.point))
            {
                // Destroy the old focus prefab if it exists
                if (lastInstantiatedObject != null)
                {
                    //Destroy(lastInstantiatedObject);
                    ReturnToPool(lastInstantiatedObject);
                }

                // Instantiate a new focus prefab at the hit point
                lastInstantiatedObject = GetFromPool(hit.point, Quaternion.identity);

                // Update the last hit info
                hitInfo = hit;
            }
            // If no point is detected and there's a last instantiated object, destroy it
            else if (!hasHit && lastInstantiatedObject != null)
            {
                ReturnToPool(lastInstantiatedObject);
                lastInstantiatedObject = null;
            }
        }
        
        public Vector3? GetCurrentZipPosition()
        {
            if (lastInstantiatedObject != null)
            {
                return lastInstantiatedObject.transform.position;
            }
            return null;
        }
        
        private void InitializePool()
        {
            pool = new List<GameObject>();
            for (int i = 0; i < poolSize; i++)
            {
                GameObject obj = Instantiate(zipPointFocusPrefab, poolSizeHolder);
                obj.SetActive(false);
                pool.Add(obj);
            }
        }

        public GameObject GetFromPool(Vector3 position, Quaternion rotation)
        {
            foreach (GameObject obj in pool)
            {
                if (!obj.activeInHierarchy)
                {
                    obj.transform.position = position;
                    obj.transform.rotation = rotation;
                    obj.SetActive(true);
                    return obj;
                }
            }

            // Optionally expand the pool if all objects are active
            GameObject newObj = Instantiate(zipPointFocusPrefab, position, rotation);
            pool.Add(newObj);
            return newObj;
        }

        public void ReturnToPool(GameObject obj)
        {
            obj.SetActive(false);
        }
    }
}