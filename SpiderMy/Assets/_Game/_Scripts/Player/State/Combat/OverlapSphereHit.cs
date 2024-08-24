using _Game.Scripts.Event;
using SFRemastered._Game._Scripts.Enemy;
using SFRemastered._Game._Scripts.Enemy.State;
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
        [SerializeField] private float knockBackradius;
        [SerializeField] private LayerMask layer;

        public GameEvent addCounterCombo;
        
        public void Hit(float damage)
        {
            Collider[] hitColliders = Physics.OverlapSphere(center.position,radius, layer);
            foreach (var hitCollider in hitColliders)
            {
                var enemy = hitCollider.GetComponent<EnemyController>();
                if (!enemy.GetComponent<EnemyBlackBoard>().die)
                {
                    enemy.OnStaggerHit(damage);
                }
            }

            if (hitColliders.Length > 0)
            {
                addCounterCombo.Raise();
            }
        }
        
        public void KnockBackHit(float damage)
        {
            Collider[] hitColliders = Physics.OverlapSphere(center.position ,knockBackradius, layer);
            foreach (var hitCollider in hitColliders)
            {
                var enemy = hitCollider.GetComponent<EnemyController>();
                if (!enemy.GetComponent<EnemyBlackBoard>().die)
                {
                    enemy.OnStaggerHit(damage);
                }
            }
            if (hitColliders.Length > 0)
            {
                addCounterCombo.Raise();
            }
        }
        
        public void UltimateHit(Transform transform, float damage)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.transform.position ,15f, layer);
            foreach (var hitCollider in hitColliders)
            {
                var enemy = hitCollider.GetComponent<EnemyController>();
                if (!enemy.GetComponent<EnemyBlackBoard>().die)
                {
                    enemy.OnStaggerHit(damage);
                }
            }
            if (hitColliders.Length > 0)
            {
                addCounterCombo.Raise();
            }
        }
        
        
        
        /*private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(center.position, 0.3f);
        }*/   
    }
}