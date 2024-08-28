using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace SFRemastered._Game._Scripts.Data
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/Data/PlayerData")]
    public class PlayerDataSO : ScriptableObject
    {
        [Header("Stats Data")]
        public int level;
        public float maxHealth;
        public float damage;
        public float cooldown;
        public float additionCoefficientHealth;
        public float additionCoefficientDamage;
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
        
        [Header("Time Data")]
        public int currentDay;
        //public DateTime lastClaimedDate;
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
            //this.exp += exp * multiplierXp;
            this.exp += exp ;
            if (this.exp >= xpToNextLevel)
            {
                this.exp -= xpToNextLevel;
                level++;
                GetCurrentCoefficient();
                GetXpToNextLevel();
                UpdateStats();
            }
        }
        
        public void AddCash(float cash)
        {
            this.cash += cash;
        }
        
        public void UpdateStats()
        {
            maxHealth = 500 + level * additionCoefficientHealth;
            damage = 20 + level * additionCoefficientDamage;
        }
    }
}