using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SFRemastered._Game._Scripts.Player.State.Combat.Gadget
{
    [CreateAssetMenu(menuName = "ScriptableObjects/ProjectileData")]
    public class ProjectileData : ScriptableObject
    {
        public GameObject projectilePrefab;
        public Queue<GameObject> PoolProjectileData = new Queue<GameObject>();
        
        public GameObject Spawn(Vector3 position, Quaternion rotation, Transform parent)
        {
            if (PoolProjectileData.Count > 0)
            {
                GameObject item = PoolProjectileData.Dequeue();
                item.transform.position = position;
                item.transform.rotation = rotation;
                item.transform.SetParent(parent);
                item.gameObject.SetActive(true);
                return item;
            }
            else
            {
                return Instantiate(projectilePrefab, position, rotation, parent);
            }
        }

        public void ReturnToPool(GameObject item)
        {
            PoolProjectileData.Enqueue(item);
            item.gameObject.SetActive(false);
        }
    }
}