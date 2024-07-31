using System.Collections;
using System.Collections.Generic;
using SFRemastered._Game._Scripts.Manager;
using UnityEngine;

namespace SFRemastered
{
    public abstract class QuestStep : MonoBehaviour
    {
        private bool isFinished = false;

        private string questId;
        
        public void InitializeQuestStep(string questId)
        {
            this.questId = questId;
        }
        
        protected void FinishQuestStep()
        {
            isFinished = true;
            
            GameEventManager.instance.questEvent.AdvanceQuest(questId);
            
            Destroy(this.gameObject);
        }
    }
}
