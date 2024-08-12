using System;
using System.Collections;
using _Game.Scripts.Event;
using Animancer;
using NodeCanvas.Framework;
using SFRemastered._Game._Scripts.Enemy.State;
using UnityEngine;

namespace SFRemastered._Game._Scripts.Enemy
{
    public class EnemyController : MonoBehaviour, IHitable
    {
        public float health;
        [SerializeField] private bool getHit;
        [SerializeField] public bool zipAttackStun;
        
        [SerializeField] private HealthBar healthBar;
        [SerializeField] private EnemyBlackBoard blackBoard;
        
        private Vector3 velocity;
        public float gravity = -9.81f;
        
        [Header("Events")]
        public GameEvent onEnemyDeath;
        public GameEvent onStartAttack;
        public GameEvent onEndAttack;
        //[Header("Events Listener")]
        //public GameEventListener onStaggerHit;

        private void Start()
        {
           
        }

        public void test(){}

        private void Update()
        {
            
            if (!blackBoard.characterController.isGrounded)
            {
                velocity.y += gravity * Time.deltaTime;
            }
            else
            {
                velocity.y = 0;
            }

            blackBoard.characterController.Move(velocity * Time.deltaTime);
        }

        public void OnStaggerHit(float damage)
        {
            health -= damage;
            healthBar.TakeDamage(damage);
            blackBoard.staggerHit = true;
            CheckHealth();
        }
        
        public void OnKnockBackHit(float damage)
        {
            health -= damage;
            healthBar.TakeDamage(damage);
            blackBoard.knockBackHit = true;
            CheckHealth();
        }
        
        private void CheckHealth()
        {
            if (health <= 0)
            {
                healthBar.gameObject.SetActive(false);
                onEnemyDeath.Raise();
                blackBoard.die = true;
                //Die();
            }
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}