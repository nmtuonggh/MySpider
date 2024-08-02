using System;
using System.Collections.Generic;
using UnityEngine;

namespace SFRemastered._Game._Scripts.Mission
{
    public class MainMissionController : MonoBehaviour
    {
        public List<Transform> listMissionPoint;
        public MainMissionSO mainMissionData;
        
        public BaseMission currentMission;

        private void Start()
        {
            //TODO : Bo cai nay luc lam xong
            mainMissionData.currentMissionIndex = 0;
            
            StartNextMission();
        }

        private void Update()
        {
            if (currentMission!= null)      
            {
                currentMission.UpdateMission();
            }
        }

        private void StartNextMission()
        {
            currentMission = mainMissionData.GetCurrentMission();
            if (currentMission != null)
            {
                currentMission.OnMissionComplete += HandleMissionComplete;
                currentMission.OnMissionFail += HandleMissionFail;
                currentMission.spawnPosition = listMissionPoint[mainMissionData.currentMissionIndex];
                currentMission.StartMission();
            }
        }
        
        private void HandleMissionComplete()
        {
            currentMission.OnMissionComplete -= HandleMissionComplete;
            currentMission.OnMissionFail -= HandleMissionFail;
            
            mainMissionData.AdvanceMission();
            StartNextMission();
        }

        private void HandleMissionFail()
        {
            currentMission.OnMissionComplete -= HandleMissionComplete;
            currentMission.OnMissionFail -= HandleMissionFail;
            
            //TODO: Logic to handle mission failure
        }
    }
}