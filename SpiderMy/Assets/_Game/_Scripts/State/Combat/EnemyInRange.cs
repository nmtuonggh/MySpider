using System.Collections.Generic;
using UnityEngine;

namespace SFRemastered._Game._Scripts.State.Combat
{
    public class EnemyInRange : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _listEnemy;
        [SerializeField] private BlackBoard _blackBoard;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                _listEnemy.Add(other.gameObject);
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                _listEnemy.Remove(other.gameObject);
            }
        }

        private void Update()
        {
            if (_listEnemy.Count > 0)
            {
                _blackBoard._detectedEnemy = true;
                _blackBoard._targetEnemy = FindClosestEnemy();
                _blackBoard._distanceToTargetEnemy = Vector3.Distance(transform.position, _blackBoard._targetEnemy.transform.position);
            }else
            {
                _blackBoard._detectedEnemy = false;
            }
        }
        
        private GameObject FindClosestEnemy()
        {
            float minDistance = Mathf.Infinity;
            GameObject closestEnemy = null;
            foreach (var enemy in _listEnemy)
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestEnemy = enemy;
                }
            }

            return closestEnemy;
        }
    }
}