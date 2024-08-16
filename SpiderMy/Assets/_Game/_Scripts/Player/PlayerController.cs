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
            StartCoroutine(HandleStaggerHit());
            health -= damage;
            healthBar.TakeDamage(damage);
            CheckHealth();
        }
        
        public void OnKnockBackHit(float damage)
        {
            StartCoroutine(HandleKnockBackHit());
            health -= damage;
            healthBar.TakeDamage(damage);
            CheckHealth();
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

        #endregion
    }
}