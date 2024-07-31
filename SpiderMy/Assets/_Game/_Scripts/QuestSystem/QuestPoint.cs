using System;
using SFRemastered._Game._Scripts.Manager;
using UnityEngine;

namespace SFRemastered
{
    public class QuestPoint : MonoBehaviour
    {
        [Header("Quest")]
        [SerializeField] private QuestInforSO questInforSO;
        
        [Header("Config")]
        [SerializeField] private bool startQuest;
        [SerializeField] private bool endQuest;
        
        
        private bool playerIsNear = false;
        private string questId;
        private QuestState currentQuestState;

        private void Awake()
        {
            questId = questInforSO.id;
        }

        private void OnEnable()
        {
            GameEventManager.instance.questEvent.onQuestStateChange += QuestStateChange;
        }
        
        private void OnDisable()
        {
            GameEventManager.instance.questEvent.onQuestStateChange -= QuestStateChange;
            
        }
         
        private void SubmitQuest()
        {
            if (!playerIsNear)
            {
                return;
            }
            
        }
        
        private void QuestStateChange(Quest quest)
        {
            if (quest.questInfor.id.Equals(questId))
            {
                currentQuestState = quest.questState;
            }
            
            GameEventManager.instance.questEvent.AdvanceQuest(questId);
            GameEventManager.instance.questEvent.FinishQuest(questId);

            if (currentQuestState.Equals(QuestState.CanStart) && startQuest)
            {
                GameEventManager.instance.questEvent.StartQuest(questId);
            }
            else if (currentQuestState.Equals(QuestState.CanFinish) && endQuest)
            {
                GameEventManager.instance.questEvent.FinishQuest(questId);
            }
            
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                playerIsNear = true;
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                playerIsNear = false;
            }
        }
    }
}