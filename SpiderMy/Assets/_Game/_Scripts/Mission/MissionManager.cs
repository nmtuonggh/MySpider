using System;
using System.Collections.Generic;
using _Game.Scripts.Event;
using UnityEngine;

namespace SFRemastered._Game._Scripts.Mission
{
    public class MissionManager : MonoBehaviour
    {
        public List<Transform> missionPositions;
        public MainMissionSO mainMissionSO;
        public BlackBoard playerBlackBoard;

        public BaseMissionSO currentMission;
        private GameObject currentMissionPrefab;
        
        [Header("==========Mission UI==========")]
        public UI_ShowMission MissionUIScript;
        public GameObject missionUI;
        
        [Header("==========Mission Event==========")]
        //public GameEvent onS;
        
        [Header("==========Mission Event Listener==========")]
        public GameEventListener onMissionStart;
        public GameEventListener onMissionUpdate;
        public GameEventListener onMissionComplete;
        public GameEventListener onMissionFail;
        public GameEventListener onMissionFailByDie;
        public GameEventListener onNotRevive;

        private void OnEnable()
        {
            onMissionStart.OnEnable();
            onMissionUpdate.OnEnable();
            onMissionComplete.OnEnable();
            onMissionFail.OnEnable();
            onMissionFailByDie.OnEnable();
        }


        private void OnDisable()
        {
            onMissionStart.OnDisable();
            onMissionUpdate.OnDisable();
            onMissionComplete.OnDisable();
            onMissionFail.OnDisable();
            onMissionFailByDie.OnDisable();
        }

        private void Start()
        {
            //TODO Fix it when build
            mainMissionSO.currentMissionIndex = 0;
            StartMission();
        }

        public void HandlerMissionStart()
        {
            
        }

        public void HandlerMissionUpdate()
        {
        }

        public void HandlerMissionComplete()
        {
            if (mainMissionSO.currentMissionIndex < mainMissionSO.listMission.Count - 1)
            {
                mainMissionSO.AdvanceMission();
                Destroy(currentMissionPrefab);
                MissionUIScript.HandlerMissionComplete();
            }
            else
            {
                mainMissionSO.currentMissionIndex = 0;
                Destroy(currentMissionPrefab);
                MissionUIScript.HandlerMissionComplete();
            }
        }

        public void HandlerMissionFail()
        {
            Destroy(currentMissionPrefab);
            playerBlackBoard.spiderSen.spiderSen.gameObject.SetActive(false);
            playerBlackBoard.spiderSen.spiderSenCount=0;
            MissionUIScript.HandlerMissionFail();
        }
        public void HandlerNotRevive()
        {
            Destroy(currentMissionPrefab);
            playerBlackBoard.spiderSen.spiderSen.gameObject.SetActive(false);
            playerBlackBoard.spiderSen.spiderSenCount=0;
        }
        public void HandlerMissionFailByDead()
        {
            MissionUIScript.HandlerMissionFailByDie();
        }

        public void StartMission()
        {
           
            currentMission = mainMissionSO.GetCurrentMission();
            currentMission.GetMissionPosition(missionPositions[mainMissionSO.currentMissionIndex]);
            currentMissionPrefab = Instantiate(currentMission.missionPrefab, currentMission.SpawnPosition.position,
                Quaternion.identity);
            currentMissionPrefab.transform.SetParent(transform);
            currentMissionPrefab.GetComponent<BaseMission>().StartMission();
            MissionUIScript.HandlerMissionStart();
        }
    }
}