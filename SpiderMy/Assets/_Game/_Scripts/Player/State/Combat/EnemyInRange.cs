using System.Collections.Generic;
using SFRemastered._Game._Scripts.Enemy;
using SFRemastered._Game._Scripts.Enemy.State;
using UnityEngine;

namespace SFRemastered._Game._Scripts.State.Combat
{
    public class EnemyInRange : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _listEnemy;
        [SerializeField] private BlackBoard _blackBoard;
        [SerializeField] private float checkRadius;
        [SerializeField] private LayerMask enemyLayer;
        private bool _isTriggerActive;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                _listEnemy.Add(other.gameObject);
                _isTriggerActive = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                _listEnemy.Remove(other.gameObject);
                if (_listEnemy.Count == 0)
                {
                    _isTriggerActive = false;
                }
            }
        }

        private void Update()
        {
            if (_isTriggerActive)
            {
                CheckEnemiesInRange();
            }

            if (_listEnemy.Count > 0)
            {
                _blackBoard._detectedEnemy = true;
                _blackBoard._targetEnemy = FindClosestEnemy();
                _blackBoard._closestEnemyNotStun = FindClosestEnemyNotStun();
                _blackBoard._distanceToTargetEnemy = Vector3.Distance(transform.position, _blackBoard._targetEnemy.transform.position);
            }
            else
            {
                _blackBoard._detectedEnemy = false;
                _blackBoard._targetEnemy = null;
            }
        }

        private void CheckEnemiesInRange()
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, checkRadius, enemyLayer);
            _listEnemy.Clear();
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.gameObject.CompareTag("Enemy"))
                {
                    _listEnemy.Add(hitCollider.gameObject);
                }
            }
        }

        private GameObject FindClosestEnemy()
        {
            float minDistance = Mathf.Infinity;
            GameObject closestEnemy = null;
            foreach (var enemy in _listEnemy)
            {
                if (enemy != null)
                {
                    float distance = Vector3.Distance(transform.position, enemy.transform.position);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        closestEnemy = enemy;
                    }
                }
            }

            return closestEnemy;
        }
        
        private GameObject FindClosestEnemyNotStun()
        {
            float minDistance = Mathf.Infinity;
            GameObject closestEnemy = null;
            foreach (var enemy in _listEnemy)
            {
                if (enemy != null && !enemy.GetComponent<EnemyBlackBoard>().webHitStun)
                {
                    float distance = Vector3.Distance(transform.position, enemy.transform.position);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        closestEnemy = enemy;
                    }
                }
            }

            return closestEnemy;
        }
    }
}