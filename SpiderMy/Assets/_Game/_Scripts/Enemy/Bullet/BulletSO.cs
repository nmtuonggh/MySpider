using System.Collections.Generic;
using UnityEngine;

namespace SFRemastered._Game._Scripts.Enemy.Bullet
{
    [CreateAssetMenu(menuName = "ScriptableObjects/BulletData")]
    public class BulletSO :ScriptableObject
    {
        public GameObject prefab;
        public Queue<GameObject> pool = new Queue<GameObject>();
        
        public GameObject Spawn(Vector3 position, Quaternion rotation, Transform parent)
        {
            if (pool.Count > 0)
            {
                GameObject item = pool.Dequeue();
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
            pool.Enqueue(item);
            item.gameObject.SetActive(false);
        }
    }
}