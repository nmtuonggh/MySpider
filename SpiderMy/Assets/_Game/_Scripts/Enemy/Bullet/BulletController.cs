using System;
using UnityEngine;

namespace SFRemastered._Game._Scripts.Enemy.Bullet
{
    public class BulletController : MonoBehaviour
    {
        [SerializeField] private EnemySO ownerEnemyData;
        [SerializeField] private BulletSO bulletSo;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (!other.GetComponent<BlackBoard>().invincible)
                {
                    other.GetComponent<PlayerController>().OnStaggerHit(ownerEnemyData.damage);
                }
                bulletSo.ReturnToPool(gameObject);
            }
        }
    }
}