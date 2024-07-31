using System;
using Unity.VisualScripting;
using UnityEngine;

namespace SFRemastered.QuestSteps
{
    public class FightingQuestStep : QuestStep
    {
        public int _enemyCount = 5;

        private void EnemyDie()
        {
            _enemyCount -= 1;
            if (_enemyCount <=0)
            {
                FinishQuestStep();
            }
        }

        private void Update()
        {
            if (_enemyCount <=0)
            {
                FinishQuestStep();
            }
        }
    }
}