﻿using System;
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
        
        
        public bool playerIsNear = false;
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

        private void Update()
        {
        }

        private void SubmitQuest()
        {
            if (!playerIsNear)
            {
                return;
            }
            
            if (currentQuestState.Equals(QuestState.CanStart) && startQuest)
            {
                GameEventManager.instance.questEvent.StartQuest(questId);
            }
            /*else if (currentQuestState.Equals(QuestState.CanFinish) && endQuest)
            {
                GameEventManager.instance.questEvent.FinishQuest(questId);
                Debug.Log("Finishing quest with id: " + questId);
            }*/
        }
        
        private void QuestStateChange(Quest quest)
        {
            if (quest.questInfor.id.Equals(questId))
            {
                currentQuestState = quest.questState;
            }

            //SubmitQuest();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                playerIsNear = true;
                SubmitQuest();
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