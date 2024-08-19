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

            _blackBoard._detectedEnemy = _listEnemy.Count > 0;
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

        public GameObject FindClosestEnemy()
        {
            if (_listEnemy.Count > 0)
            {
                float minDistance = Mathf.Infinity;
                GameObject closestEnemy = null;
                foreach (var enemy in _listEnemy)
                {
                    
                    if (enemy != null 
                        && (!enemy.GetComponent<EnemyBlackBoard>().die && !enemy.GetComponent<EnemyBlackBoard>().knockBackHit))
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
            else
            {
                return null;
            }
        }

        public GameObject FindClosestEnemyNotStun()
        {
            if (_listEnemy.Count > 0)
            {
                float minDistance = Mathf.Infinity;
                GameObject closestEnemy = null;
                foreach (var enemy in _listEnemy)
                {
                    if (!enemy.GetComponent<EnemyBlackBoard>().stunLockHit 
                        && !enemy.GetComponent<EnemyBlackBoard>().die 
                        && !enemy.GetComponent<EnemyBlackBoard>().knockBackHit)
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
            else
            {
                return null;
            }
        }
        
        public float GetDistanceToClosetEnemy()
        {
            if (FindClosestEnemy()!=null)
            {
                return Vector3.Distance(transform.position, FindClosestEnemy().transform.position);
            }

            return -1;
        }
        
    }
}