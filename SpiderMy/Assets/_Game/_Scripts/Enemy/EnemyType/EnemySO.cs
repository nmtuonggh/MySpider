using System.Collections.Generic;
using UnityEngine;

namespace SFRemastered._Game._Scripts.Enemy
{
    public class EnemySO : ScriptableObject
    {
        public GameObject prefab;
        
        public float health;
        public float damage;
        public float speed;
        
        public float attackRange;
        public float attackCooldown;
        
        public Queue<GameObject> EnemyPool = new Queue<GameObject>();
        
        public GameObject Spawn(Vector3 position, Quaternion rotation, Transform parent)
        {
            if (EnemyPool.Count > 0)
            {
                GameObject item = EnemyPool.Dequeue();
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

        public void ReturnToPool(GameObject item)
        {
            EnemyPool.Enqueue(item);
            item.gameObject.SetActive(false);
        }
    }
}