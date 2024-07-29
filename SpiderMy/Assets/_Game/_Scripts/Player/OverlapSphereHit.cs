﻿using SFRemastered._Game._Scripts.Enemy;
using SFRemastered._Game.ScriptableObjects.AnimationAttack;
using UnityEngine;

namespace SFRemastered
{
    public class OverlapSphereHit : MonoBehaviour
    {
        [SerializeField] private Transform start;
        [SerializeField] private Transform end;
        [SerializeField] private Transform center;
        [SerializeField] private float radius;
        [SerializeField] private LayerMask layer;
        
        public void Hit(float damage)
        {
            Collider[] hitColliders = Physics.OverlapCapsule(start.transform.position, end.transform.position,radius, layer);
            foreach (var hitCollider in hitColliders)
            {
                var enemy = hitCollider.GetComponent<IHitable>();
                enemy.OnHit(damage);
            }
        }
        
        public void MidKickHit(float damage)
        {
            Collider[] hitColliders = Physics.OverlapSphere(center.transform.position ,0.2f, layer);
            foreach (var hitCollider in hitColliders)
            {
                var enemy = hitCollider.GetComponent<IHitable>();
                enemy.OnHit(damage);
            }
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(start.position, radius);
            Gizmos.DrawWireSphere(end.position, radius);
        }   
    }
}