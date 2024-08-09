using System.Collections.Generic;
using UnityEngine;

namespace SFRemastered._Game._Scripts.Player.State.Combat.Gadget
{
    [CreateAssetMenu(menuName = "ScriptableObjects/HealingBotSO")]
    public class HealingBotSO : ScriptableObject
    {
        public GameObject healingBotPrefab;
        public Queue<GameObject> poolHealingBot = new Queue<GameObject>();
        
        public GameObject Spawn(Vector3 position, Quaternion rotation, Transform parent)
        {
            if (poolHealingBot.Count > 0)
            {
                GameObject item = poolHealingBot.Dequeue();
                item.transform.position = position;
                item.transform.rotation = rotation;
                item.transform.SetParent(parent);
                item.gameObject.SetActive(true);
                return item;
            }
            else
            {
                return Instantiate(healingBotPrefab, position, rotation, parent);
            }
        }

        public void ReturnToPool(GameObject item)
        {
            poolHealingBot.Enqueue(item);
            item.gameObject.SetActive(false);
        }
    }
}