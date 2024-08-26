using System;
using System.Collections;
using _Game.Scripts.Event;
using Animancer;
using DamageNumbersPro;
using DG.Tweening;
using NodeCanvas.Framework;
using SFRemastered._Game._Scripts.Enemy.State;
using Unity.VisualScripting;
using UnityEngine;

namespace SFRemastered._Game._Scripts.Enemy
{
    public class EnemyController : MonoBehaviour, IHitable
    {
        [Header("=============Enemy Stats============")]
        public EnemySO.EnemyType enemyType;
        public float health;
        
        [SerializeField] private HealthBar healthBarscript;
        [SerializeField] private EnemyBlackBoard blackBoard;
        [SerializeField] private DamageNumber damageNumber;
        [SerializeField] private ParticleSystem hitPrefab;
        
        private Vector3 velocity;
        public float gravity = -9.81f;
        
        [Header("=============Events=============")]
        public GameEvent onEnemyDeath;
        public GameEvent onDisableSpiderSense;
        public GameEvent onEndAttack;

        private void Start()
        {
            
        }

        private void OnEnable()
        {
            blackBoard.healthBarUI.SetActive(true);
            health = blackBoard.enemyData.health;
            healthBarscript.healthBar.maxValue = health;
            healthBarscript.easeHealthBar.maxValue = health;
            blackBoard.attackCoolDown = blackBoard.enemyData.attackCooldown;
        }


        private void Update()
        {
            
            if (!blackBoard.characterController.isGrounded && !blackBoard.disableRB)
            {
                velocity.y += gravity * Time.deltaTime;
            }
            /*else
            {
                velocity.y = 0;
            }*/

            blackBoard.characterController.Move(velocity * Time.deltaTime);
            
            blackBoard.targetHealth = blackBoard.target.obj.GetComponent<PlayerController>().health;
            blackBoard.targetInvincible = blackBoard.target.obj.GetComponent<BlackBoard>().invincible;
        }
        

        public void OnStaggerHit(float damage)
        {
            if (!blackBoard.blocking || !blackBoard.invincible)
            {
                damageNumber.Spawn(this.transform.position + Vector3.up, damage);
                //hitPrefab.Play();
                HitEffect();
                health -= damage;
                healthBarscript.TakeDamage(damage);
                blackBoard.staggerHit = true;
                CheckHealth();
                ControlHitStatus();
                DisableSpiderSense();
            }
        }
        
        public void OnKnockBackHit(float damage)
        {
            if (!blackBoard.blocking || !blackBoard.invincible)
            {
                Debug.Log("run");
                damageNumber.Spawn(this.transform.position + Vector3.up, damage);
                //hitPrefab.Play();
                HitEffect();
                health -= damage;
                healthBarscript.TakeDamage(damage);
                blackBoard.knockBackHit = true;
                ControlHitStatus();
                CheckHealth();
                DisableSpiderSense();
            }
        }
        
        private void HitEffect()
        {
            var hit = blackBoard.poolObject.Spawn(this.transform.position + Vector3.up*0.8f, Quaternion.identity, this.transform);
            DOVirtual.DelayedCall(0.6f, () => { blackBoard.poolObject.ReturnToPool(hit);});
        }
        
        private void CheckHealth()
        {
            if (health <= 0)
            {
                onEnemyDeath.Raise();
                blackBoard.die = true;
            }
        }
        
        public void ControlHitStatus()
        {
            if (blackBoard.hitStatus < 5)
            {
                blackBoard.hitStatus += 1;
            }
            else
            {
                StartCoroutine(HandleHitStatus());
            }
        }
        
        private IEnumerator HandleHitStatus()
        {
            yield return new WaitForSeconds(3f);
            blackBoard.hitStatus = 0;
        }

        private void DisableSpiderSense()
        { 
            if (blackBoard.attacking)
            {
                blackBoard.attacking = false;
                onDisableSpiderSense.Raise();
                blackBoard.warningAttack.SetActive(false);
            }
        }
    }
}