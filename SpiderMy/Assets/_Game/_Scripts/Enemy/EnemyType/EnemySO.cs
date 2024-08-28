using System.Collections.Generic;
using UnityEngine;

namespace SFRemastered._Game._Scripts.Enemy
{
    public class EnemySO : ScriptableObject
    {
        public enum EnemyType
        {
            Melee,
            Ranged,
            Boss
        }
        
        [Header("Enemy Data")]
        public EnemyType enemyType;
        public int id;
        public GameObject prefab;
        public float health;
        public float damage;
        public float speed;
        
        public float attackRange;
        public float attackCooldown;
        [Header("Enemy Stats")]
        public int level;
        public float additionCoefficientHealth;
        public float additionCoefficientDamage;

        public void UpdateLevel(int level)
        {
            this.level = level;
            UpdateStats(this.level);
        }
        
        public void UpdateStats(int level)
        {
            health = 100 + level * additionCoefficientHealth;
            damage = 10 +  level * additionCoefficientDamage;
        }
        
        
        public Dictionary<int, Queue<GameObject>> enemyPools = new Dictionary<int, Queue<GameObject>>();
        
        public GameObject Spawn(Vector3 position, Quaternion rotation, Transform parent, int id)
        {
            if (enemyPools.ContainsKey(id) && enemyPools[id].Count > 0)
            {
                GameObject item = enemyPools[id].Dequeue();
                item.transform.position = position;
                item.transform.rotation = rotation;
                item.transform.SetParent(parent);
                item.gameObject.SetActive(true);
                return item;
            }
            else
            {
                return Instantiate(prefab, position, rotation, parent);
            }
        }

        public void ReturnToPool(int id, GameObject enemy)
        {
            enemy.SetActive(false);
            if (!enemyPools.ContainsKey(id))
            {
                enemyPools[id] = new Queue<GameObject>();
            }
            enemyPools[id].Enqueue(enemy);
        }
    }
}