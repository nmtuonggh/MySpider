using UnityEngine;
using UnityEngine.Serialization;

namespace SFRemastered._Game._Scripts.Data
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/Data/PlayerData")]
    public class PlayerDataSO : ScriptableObject
    {
        [Header("Stats")]
        public int level;
        public float maxHealth;
        public float damage;
        public float cooldown;
        [Header("Player Inventory")]
        public float cash;
        public float exp;
        public float skillPoints;
        [Header("Current Data")]
        public float currentHealth;
        
        [Header("Level Data")]
        public int currentCoefficient;
        public float xpToNextLevel;
        public float multiplierXp = 1;
        public float timeMulti;
        
        [Header("Suit Data")]
        public int cardOwned;
        
        public int currentDay;
        public void GetCurrentCoefficient()
        {
            if (level < 4)
            {
                currentCoefficient = 10 + (level-1) * 5;
            }
            else
            {
                currentCoefficient = 22 + (level-4) * 2;
            }
        }

        public void SetCurrentLevel(int level)
        {
            this.level = level;
        }
        
        public void GetXpToNextLevel()
        {
            xpToNextLevel = (level + currentCoefficient) * (level + currentCoefficient);
        }
        
        public void AddExp(float exp)
        {
            this.exp += exp * multiplierXp;
            if (this.exp >= xpToNextLevel)
            {
                this.exp -= xpToNextLevel;
                level++;
                GetCurrentCoefficient();
                GetXpToNextLevel();
            }
        }
        
        public void AddCash(float cash)
        {
            this.cash += cash;
        }
    }
}