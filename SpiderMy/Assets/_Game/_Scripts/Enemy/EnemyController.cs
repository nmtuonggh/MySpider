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

        public void OnHit(float damage)
        {
            health -= damage;
            healthBar.TakeDamage(damage);
            StartCoroutine(GetHit());
            _bb.SetVariableValue("hit", true);

            if (health <= 0)
            {
                onEnemyDeath.Raise();
                Die();
            }
        }

        private IEnumerator GetHit()
        {
            getHit = true;
            _bb.SetVariableValue("hit", true);
            yield return new WaitForSeconds(0.15f);
            _bb.SetVariableValue("hit", false);
            getHit = false;
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}