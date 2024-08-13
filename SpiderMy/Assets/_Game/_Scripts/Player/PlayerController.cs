using SFRemastered._Game._Scripts.Enemy;
using UnityEngine;

namespace SFRemastered
{
    public class PlayerController : MonoBehaviour, IHitable
    {
        [Header("=============Player Stats============")]
        public float health;
        public PlayerHealthBar healthBar;
        public BlackBoard blackBoard;
        
        
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
                Debug.Log("Die");
            }
        }
    }
}