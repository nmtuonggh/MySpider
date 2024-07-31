using System;
using System.Collections.Generic;
using SFRemastered._Game._Scripts.Manager;
using UnityEngine;

namespace SFRemastered._Game._Scripts.QuestSystem
{
    public class QuestManager : MonoBehaviour
    {
        private Dictionary<string, Quest> questMap;

        private void Awake()
        {
            questMap = CreateQuestMap();
        }

        private void OnEnable()
        {
            GameEventManager.instance.questEvent.onStartQuest += StartQuest;
            GameEventManager.instance.questEvent.onAdvanceQuest += AdvanceQuest;
            GameEventManager.instance.questEvent.onFinishQuest += FinishQuest;
        }
        
        private void OnDisable()
        {
            GameEventManager.instance.questEvent.onStartQuest -= StartQuest;
            GameEventManager.instance.questEvent.onAdvanceQuest -= AdvanceQuest;
            GameEventManager.instance.questEvent.onFinishQuest -= FinishQuest;
        }

        private void Start()
        {
            foreach (var quest in questMap.Values)
            {
                GameEventManager.instance.questEvent.QuestStateChange(quest);
            }
        }

        private void Update()
        {
            foreach (var quest in questMap.Values)
            {
                if (quest.questState == QuestState.RequirementsNotMet)
                {
                    ChangeQuestState(quest.questInfor.id, QuestState.CanStart);
                }
            }
        }

        private void ChangeQuestState(string id, QuestState state)
        {
            Quest quest = GetQuestByID(id);
            quest.questState = state;
            GameEventManager.instance.questEvent.QuestStateChange(quest);
        }

        private void StartQuest(string id)
        {
            Debug.Log("Starting quest with id: " + id);
            Quest quest = GetQuestByID(id);
            quest.InstantiateCurrentQuestStep(this.transform);
            ChangeQuestState(quest.questInfor.id, QuestState.InProgress);
        }
        private void AdvanceQuest(string id)
        {
            Debug.Log("Advancing quest with id: " + id);
           Quest quest = GetQuestByID(id);
           quest.MoveToNextStep();

           if (quest.CurrentStepExits())
           {
               quest.InstantiateCurrentQuestStep(this.transform);
           }
           else
           {
               ChangeQuestState(quest.questInfor.id, QuestState.CanFinish);
           }
        }
        private void FinishQuest(string id)
        {
            Debug.Log("Finishing quest with id: " + id);
            Quest quest = GetQuestByID(id);
            ClaimReward(quest);
            ChangeQuestState(quest.questInfor.id, QuestState.Finished);
        }

        private void ClaimReward(Quest quest)
        {
            //TODO: Implement reward system
            Debug.Log("Quest finished u got: " + quest.questInfor.cashReward + " cash" + quest.questInfor.expReward + " exp");
        }

        private Dictionary<string, Quest> CreateQuestMap()
        {
            //load all questsinforSO SO under the folder
            //QuestInforSO[] allQuests = Resources.LoadAll<QuestInforSO>("Assets/_Game/Resources/Quest");
            QuestInforSO[] allQuests = Resources.LoadAll<QuestInforSO>("Quest");
            //create questmap
            Dictionary<string, Quest> idToQuestMap = new Dictionary<string, Quest>();
            foreach(QuestInforSO questInfor in allQuests)
            {
                if (idToQuestMap.ContainsKey(questInfor.id))
                {
                    Debug.LogWarning("Duplicate quest id found when creating map: " + questInfor.id);
                }
                idToQuestMap.Add(questInfor.id, new Quest(questInfor));
            }

            return idToQuestMap;
        }

        private Quest GetQuestByID(string id)
        {
            Quest quest = questMap[id];
            if (quest == null)
            {
                Debug.LogError("Quest not found with id: " + id);
            }
            return quest;
        }
    }
}
