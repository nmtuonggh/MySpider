using System.Collections;
using DamageNumbersPro;
using SFRemastered._Game._Scripts.Enemy;
using UnityEngine;

namespace SFRemastered
{
    public class PlayerController : MonoBehaviour, IHitable
    {
        [Header("=============Player Stats============")]
        public float health;
        public PlayerHealthBar healthBar;
        public BlackBoard _blackBoard;
        public DamageNumber damageNumber;

        #region OnHit

        public void OnStaggerHit(float damage)
        {
            damageNumber.Spawn(this.transform.position + Vector3.up, damage);
            StartCoroutine(HandleStaggerHit());
            health -= damage;
            healthBar.TakeDamage(damage);
            CheckHealth();
        }
        
        public void OnKnockBackHit(float damage)
        {
            damageNumber.Spawn(this.transform.position + Vector3.up, damage);
            StartCoroutine(HandleKnockBackHit());
            health -= damage;
            healthBar.TakeDamage(damage);
            CheckHealth();
        }
        
        public void OnVenomPhase1Hit(float damage)
        {
            damageNumber.Spawn(this.transform.position + Vector3.up, damage);
            StartCoroutine(HandleVenomPhase1Hit());
            health -= damage;
            healthBar.TakeDamage(damage);
            CheckHealth();
        }
        
        public void OnVenomPhase2Hit(float damage)
        {
            damageNumber.Spawn(this.transform.position + Vector3.up, damage);
            StartCoroutine(HandleVenomPhase2Hit());
            health -= damage;
            healthBar.TakeDamage(damage);
            CheckHealth();
        }
        
        public void OnVenomMiniHit(float damage)
        {
            damageNumber.Spawn(this.transform.position + Vector3.up, damage);
            health -= damage;
            healthBar.TakeDamage(damage);
            CheckHealth();
        }
        
        public void OnVenomFinalHit(float damage)
        {
            damageNumber.Spawn(this.transform.position + Vector3.up, damage);
            StartCoroutine(HandleVenomFinalHit());
            health -= damage;
            healthBar.TakeDamage(damage);
            CheckHealth();
        }

        #endregion
        
        
        
        private void CheckHealth()
        {
            if (health <= 0)
            {
                Debug.Log("Die");
            }
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