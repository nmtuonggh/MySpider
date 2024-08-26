using System;
using System.Collections;
using _Game.Scripts.Event;
using DamageNumbersPro;
using DG.Tweening;
using SFRemastered._Game._Scripts.Data;
using SFRemastered._Game._Scripts.Enemy;
using SFRemastered._Game._Scripts.Player.State.Combat.Gadget;
using UnityEngine;
using UnityEngine.Serialization;

namespace SFRemastered
{
    public class PlayerController : MonoBehaviour, IHitable
    {
        [Header("=============Player Stats============")]
        public float health;
        public PlayerHealthBar healthBar;
        public BlackBoard _blackBoard;
        public DamageNumber damageNumber;
        public PlayerDataSO playerData;
        
        public GameEvent onPlayerDead;

        [FormerlySerializedAs("hitVFX")] public PoolObject poolObject;
        private void OnEnable()
        {
            health = playerData.maxHealth;
        }

        #region OnHit

        private void HitEffect()
        {
            var hit = poolObject.Spawn(this.transform.position + Vector3.up*0.8f, Quaternion.identity, this.transform);
            DOVirtual.DelayedCall(0.6f, () => { poolObject.ReturnToPool(hit);});
        }
        public void OnStaggerHit(float damage)
        {
            damageNumber.Spawn(this.transform.position + Vector3.up, damage);
            HitEffect();
            StartCoroutine(HandleStaggerHit());
            health -= damage;
            playerData.currentHealth = health;
            healthBar.TakeDamage(damage);
            CheckHealth();
        }
        
        public void OnKnockBackHit(float damage)
        {
            damageNumber.Spawn(this.transform.position + Vector3.up, damage);
            HitEffect();
            StartCoroutine(HandleKnockBackHit());
            health -= damage;
            playerData.currentHealth = health;
            healthBar.TakeDamage(damage);
            CheckHealth();
        }
        
        public void OnVenomPhase1Hit(float damage)
        {
            damageNumber.Spawn(this.transform.position + Vector3.up, damage);
            HitEffect();
            StartCoroutine(HandleVenomPhase1Hit());
            health -= damage;
            playerData.currentHealth = health;
            healthBar.TakeDamage(damage);
            CheckHealth();
        }
        
        public void OnVenomPhase2Hit(float damage)
        {
            damageNumber.Spawn(this.transform.position + Vector3.up, damage);
            HitEffect();
            StartCoroutine(HandleVenomPhase2Hit());
            health -= damage;
            playerData.currentHealth = health;
            healthBar.TakeDamage(damage);
            CheckHealth();
        }
        
        public void OnVenomMiniHit(float damage)
        {
            damageNumber.Spawn(this.transform.position + Vector3.up, damage);
            HitEffect();
            health -= damage;
            playerData.currentHealth = health;
            healthBar.TakeDamage(damage);
            CheckHealth();
        }
        
        public void OnVenomFinalHit(float damage)
        {
            damageNumber.Spawn(this.transform.position + Vector3.up, damage);
            HitEffect();
            StartCoroutine(HandleVenomFinalHit());
            health -= damage;
            playerData.currentHealth = health;
            healthBar.TakeDamage(damage);
            CheckHealth();
        }

        #endregion
        
        private void CheckHealth()
        {
            if (health <= 0)
            {
                onPlayerDead.Raise();
                _blackBoard.dead = true;
            }
        }

        public void RegentHealth()
        {
            health += playerData.maxHealth;
            playerData.currentHealth = playerData.maxHealth;
        }
        
        public void StartSwingCoroutine()
        {
            StartCoroutine(ReadyToSwing());
            
        }


        #region Couroutines

        private IEnumerator ReadyToSwing()
        {
            yield return new WaitForSeconds(0.8f);
            _blackBoard.readyToSwing = true;
        }
        private IEnumerator HandleKnockBackHit()
        {
            _blackBoard.knockBackHit = true;
            yield return new WaitForSeconds(0.2f);
            _blackBoard.knockBackHit = false;
        }
        private IEnumerator HandleStaggerHit()
        {
            _blackBoard.staggerHit = true;
            yield return new WaitForSeconds(0.2f);
            _blackBoard.staggerHit = false;
        }
        
        private IEnumerator HandleVenomPhase1Hit()
        {
            _blackBoard.venomP1Hit = true;
            yield return new WaitForSeconds(0.2f);
            _blackBoard.venomP1Hit = false;
        }
        
        private IEnumerator HandleVenomPhase2Hit()
        {
            _blackBoard.venomP2Hit = true;
            yield return new WaitForSeconds(0.2f);
            _blackBoard.venomP2Hit = false;
        }
        
        private IEnumerator HandleVenomFinalHit()
        {
            _blackBoard.venomFinalHit = true;
            yield return new WaitForSeconds(0.2f);
            _blackBoard.venomFinalHit = false;
        }

        #endregion
    }
}