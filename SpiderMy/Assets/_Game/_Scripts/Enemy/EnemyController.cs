using System;
using System.Collections;
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


        //[SerializeField] private ClipTransition knockBackAnimation;

        private void Start()
        {
            animancer.Play(idleAnimation);
        }

        private void Update()
        {
            if (getHit)
            {
                animancer.Play(hitAnimation);
            }
            else
            {
                animancer.Play(idleAnimation);
            }
        }

        public void OnHit(float damage)
        {
            health -= damage;
            healthBar.TakeDamage(damage);
            getHit = true;
            StartCoroutine(GetHit());

            if (health <= 0)
            {
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