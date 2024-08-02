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

        private void OnEnable()
        {
            //currentMission.OnMissionStart += HandleMissionComplete;
            currentMission.OnMissionComplete += HandleMissionComplete;
            currentMission.OnMissionFail += HandleMissionFail;
        }

        private void OnDisable()
        {
            //currentMission.OnMissionStart -= HandleMissionComplete;
            currentMission.OnMissionComplete -= HandleMissionComplete;
            currentMission.OnMissionFail -= HandleMissionFail;
        }

        private void Start()
        {
            currentMission = mainMissionData.GetCurrentMission();
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

        /*private void DrawnMissionRange()
        {
            if (currentMission != null)
            {
                GameObject missionRange = Instantiate(currentMission.missionRange, currentMission.spawnPosition.position, Quaternion.identity);
                missionRange.transform.SetParent(currentMission.spawnPosition);
            }
        }*/

        private void DrawnIndicator()
        {
            GameObject indicator =
                Instantiate(indicatorPrefab, currentMission.spawnPosition.position, Quaternion.identity);
            indicator.transform.SetParent(currentMission.spawnPosition);
        }

        private void StartMission()
        {
            currentMission = mainMissionData.GetCurrentMission();
            if (currentMission != null)
            {
                DrawnIndicator();
                currentMission.spawnPosition = listMissionPoint[mainMissionData.currentMissionIndex];
                currentMission.StartMission();
            }
        }

        private void HandleMissionComplete()
        {
            mainMissionData.AdvanceMission();
        }

        private void HandleMissionFail()
        {
            //TODO: Logic to handle mission failure
        }
    }
}