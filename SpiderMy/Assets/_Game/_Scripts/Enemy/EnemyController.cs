using System;
using System.Collections;
using _Game.Scripts.Event;
using Animancer;
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
            getHit = true;
            StartCoroutine(GetHit());

            if (health <= 0)
            {
                Debug.Log("Enemy Die");
                onEnemyDeath.Raise();
                Die();
            }
        }

        private IEnumerator GetHit()
        {
            getHit = true;
            yield return new WaitForSeconds(0.2f);
            getHit = false;
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}