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

        private BaseMissionSO currentMission;
        private GameObject currentMissionPrefab;
        [Header("Mission Event")] public GameEventListener onMissionStart;
        public GameEventListener onMissionUpdate;
        public GameEventListener onMissionComplete;
        public GameEventListener onMissionFail;

        private void OnEnable()
        {
            onMissionStart.OnEnable();
            onMissionUpdate.OnEnable();
            onMissionComplete.OnEnable();
            onMissionFail.OnEnable();
        }


        private void OnDisable()
        {
            onMissionStart.OnDisable();
            onMissionUpdate.OnDisable();
            onMissionComplete.OnDisable();
            onMissionFail.OnDisable();
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
                StartMission();
            }
        }

        public void HandlerMissionFail()
        {
            Destroy(currentMissionPrefab);
            StartMission();
            Debug.Log("Mission Fail");
        }

        public void StartMission()
        {
            currentMission = mainMissionSO.GetCurrentMission();
            currentMission.SpawnPosition = missionPositions[mainMissionSO.currentMissionIndex];
            currentMissionPrefab = Instantiate(currentMission.missionPrefab, currentMission.SpawnPosition.position,
                Quaternion.identity);
            currentMissionPrefab.transform.SetParent(transform);
            currentMissionPrefab.GetComponent<BaseMission>().StartMission();
        }
    }
}