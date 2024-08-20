using SFRemastered._Game._Scripts.Enemy;
using SFRemastered._Game.ScriptableObjects.AnimationAttack;
using UnityEngine;

namespace SFRemastered
{
    public class OverlapSphereHit : MonoBehaviour
    {
        [SerializeField] private BlackBoard blackBoard;
        [SerializeField] private Transform start;
        [SerializeField] private Transform end;
        [SerializeField] private Transform center;
        [SerializeField] private float radius;
        [SerializeField] private LayerMask layer;
        
        
        public void Hit(float damage)
        {
            Collider[] hitColliders = Physics.OverlapSphere(center.position,radius, layer);
            foreach (var hitCollider in hitColliders)
            {
                var enemy = hitCollider.GetComponent<EnemyController>();
                enemy.OnStaggerHit(damage);
            }
        }
        
        public void KnockBackHit(float damage)
        {
            Collider[] hitColliders = Physics.OverlapSphere(center.position ,0.5f, layer);
            foreach (var hitCollider in hitColliders)
            {
                var enemy = hitCollider.GetComponent<EnemyController>();
                enemy.OnKnockBackHit(damage);
            }
        }
        
        public void UltimateHit(Transform transform, float damage)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.transform.position ,15f, layer);
            foreach (var hitCollider in hitColliders)
            {
                var enemy = hitCollider.GetComponent<EnemyController>();
                enemy.OnKnockBackHit(damage);
            }
        }
        
        
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(center.position, 0.3f);
        }   
    }
}