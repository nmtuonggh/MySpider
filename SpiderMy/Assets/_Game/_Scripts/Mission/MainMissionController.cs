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
        public GameObject indicatorPrefab;
        
        private GameObject currentIndicator;

        private void OnEnable()
        {
            currentMission = mainMissionData.GetCurrentMission();

            if(currentMission != null)
            { 
                currentMission.OnMissionComplete += HandleMissionComplete;
                currentMission.OnMissionFail += HandleMissionFail;
            }
        }

        private void OnDisable()
        {
            if(currentMission != null)
            {
                currentMission.OnMissionComplete += HandleMissionComplete;
                currentMission.OnMissionFail += HandleMissionFail;
            }
        }

        private void Start()
        {
            //TODO : Bo cai nay luc lam xong
            mainMissionData.currentMissionIndex = 0;
            StartMission();
        }

        private void Update()
        {
            if (currentMission != null)
            {
                currentMission.UpdateMission();
            }
        }
        

        private void StartMission()
        {
            currentMission = mainMissionData.GetCurrentMission();
            if (currentMission != null)
            {
                currentMission.spawnPosition = listMissionPoint[mainMissionData.currentMissionIndex];
                currentMission.StartMission();
            }
        }

        private void HandleMissionComplete()
        {
            mainMissionData.AdvanceMission();
            StartMission();
        }

        private void HandleMissionFail()
        {
            //TODO: Logic to handle mission failure
        }
    }
}