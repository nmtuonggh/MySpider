using System.Collections;
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
        
        
        public void OnStaggerHit(float damage)
        {
            health -= damage;
            healthBar.TakeDamage(damage);
            _blackBoard.staggerHit = true;
            CheckHealth();
            StartHitCoroutine(_blackBoard.staggerHit);
        }
        
        public void OnKnockBackHit(float damage)
        {
            health -= damage;
            healthBar.TakeDamage(damage);
            _blackBoard.knockBackHit = true;
            CheckHealth();
            StartHitCoroutine(_blackBoard.knockBackHit);
        }
        
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
        
        public void StartHitCoroutine(bool hitType)
        {
            StartCoroutine(Hit(hitType));
        }

        private IEnumerator ReadyToSwing()
        {
            yield return new WaitForSeconds(0.8f);
            _blackBoard.readyToSwing = true;
        }
        
        private IEnumerator Hit(bool hitType)
        {
            yield return null;
            hitType = false;
        }
    }
}