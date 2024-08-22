using UnityEngine;

namespace SFRemastered._Game._Scripts.Data
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/Data/PlayerData")]
    public class PlayerDataSO : ScriptableObject
    {
        [Header("Stats")]
        public float level;
        public float maxHealth;
        public float damage;
        public float cooldown;
        [Header("Player Inventory")]
        public float cash;
        public float exp;
        public float skillPoints;
        [Header("Current Data")]
        public float currentHealth;
    }
}