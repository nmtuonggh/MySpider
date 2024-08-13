using System;
using UnityEngine;

namespace SFRemastered._Game._Scripts.Enemy.Bullet
{
    public class BulletController : MonoBehaviour
    {
        [SerializeField] private float speed = 10f;
        [SerializeField] private float lifeTime = 2f;
        [SerializeField] private EnemySO ownerEnemyData;
        [SerializeField] private BulletSO bulletSo;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<PlayerController>().OnStaggerHit(ownerEnemyData.damage);
                bulletSo.ReturnToPool(gameObject);
            }
        }

        private void Update()
        {
            transform.position += transform.forward * (speed * Time.deltaTime);
            lifeTime -= Time.deltaTime;
            if (lifeTime <= 0)
            {
                bulletSo.ReturnToPool(gameObject);
            }
        }
    }
}