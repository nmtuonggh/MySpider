using System;
using System.Collections;
using _Game.Scripts.Event;
using Animancer;
using NodeCanvas.Framework;
using UnityEngine;

namespace SFRemastered._Game._Scripts.Enemy
{
    public class EnemyController : MonoBehaviour, IHitable
    {
        public float health;
        [SerializeField] private bool getHit;

        [SerializeField] private ClipTransition deathAnimation;
        [SerializeField] private ClipTransition hitAnimation;
        [SerializeField] private ClipTransition idleAnimation;

        [SerializeField] private AnimancerComponent animancer;
        [SerializeField] private HealthBar healthBar;
        [SerializeField] private Blackboard _bb;
        
        [Header("Events")]
        public GameEvent onEnemyDeath;

        //[SerializeField] private ClipTransition knockBackAnimation;

        private void Start()
        {
           
        }

        private void Update()
        {
            
        }

        public void OnStaggerHit(float damage)
        {
            health -= damage;
            healthBar.TakeDamage(damage);
           //StartCoroutine(GetHit("staggerHit"));
           _bb.SetVariableValue("staggerHit", true);
            if (health <= 0)
            {
                onEnemyDeath.Raise();
                Die();
            }
        }
        public void OnStunLockHit(float damage)
        {
            health -= damage;
            healthBar.TakeDamage(damage);
            //StartCoroutine(GetHit("stunLockHit"));
            _bb.SetVariableValue("stunLockHit", true);
            if (health <= 0)
            {
                onEnemyDeath.Raise();
                Die();
            }
        }

        private IEnumerator GetHit(string hitType)
        {
            getHit = true;
            _bb.SetVariableValue(hitType, true);
            yield return new WaitForSeconds(0.15f);
            _bb.SetVariableValue(hitType, false);
            getHit = false;
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}