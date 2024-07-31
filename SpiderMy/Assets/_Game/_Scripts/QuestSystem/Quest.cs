using UnityEngine;

namespace SFRemastered
{
    public class Quest
    {
        public QuestInforSO questInfor;

        public QuestState questState;
        
        private int currentQuestStepIndex = 0;
        
        public Quest(QuestInforSO questInfor)
        {
            this.questInfor = questInfor;
            this.questState = QuestState.RequirementsNotMet;
            this.currentQuestStepIndex = 0;
        }

        public void MoveToNextStep()
        {
            currentQuestStepIndex++;
        }

        public bool CurrentStepExits()
        {
            return (currentQuestStepIndex < questInfor.questStepPrefabs.Length);
        }
        
        public void InstantiateCurrentQuestStep(Transform parent)
        {
            GameObject questStepPrefab = GetCurrentQuestStepPrefab();
            if (questStepPrefab != null)
            {
                QuestStep questStep = Object.Instantiate<GameObject>(questStepPrefab, parent).GetComponent<QuestStep>();
                questStep.InitializeQuestStep(questInfor.id);
            }
        }
        
        private GameObject GetCurrentQuestStepPrefab()
        {
            GameObject questStepPrefab = null;
            if (CurrentStepExits())
            {
                questStepPrefab = questInfor.questStepPrefabs[currentQuestStepIndex];
            }else
            {
                Debug.LogError("No more quest steps");
            }

            return questStepPrefab;
        }
    }
}