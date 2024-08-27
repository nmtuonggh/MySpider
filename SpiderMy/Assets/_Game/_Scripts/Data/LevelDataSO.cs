using System;
using UnityEngine;

namespace SFRemastered._Game._Scripts.Data
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Data/LevelData")]
    public class LevelDataSO : ScriptableObject
    {
        public PlayerDataSO playerData;
        public int currentLevel;
        public int currentCoefficient;
        public float  xpToNextLevel;

        private void OnEnable()
        {
            currentLevel = playerData.level;
        }

        public void GetCurrentCoefficient(int level)
        {
            if (level < 4)
            {
                currentCoefficient += 5;
            }
            else
            {
                currentCoefficient += 2;
            }
        }

        public int GetCurrentLevel()
        {
            return currentLevel;
        }
        
        public void GetXpToNextLevel(int level, int coefficient)
        {
            xpToNextLevel = (level + coefficient) * (level + coefficient);
        }
    }
}