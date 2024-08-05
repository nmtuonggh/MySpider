using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace SFRemastered._Game._Scripts.Mission
{
    public class MainMissionController : MonoBehaviour
    {
        public List<Transform> listMissionPoint;
        public MainMissionSO mainMissionData;

        [FormerlySerializedAs("currentMission")] public BaseMissionSO currentMissionSo;
        public GameObject indicatorPrefab;
        
        private GameObject currentIndicator;

        /*private void OnEnable()
        {
            currentMissionSo = mainMissionData.GetCurrentMission();

            if(currentMissionSo != null)
            { 
                currentMissionSo.OnMissionComplete += HandleMissionSoComplete;
                currentMissionSo.OnMissionFail += HandleMissionSoFail;
            }
        }

        private void OnDisable()
        {
            if(currentMissionSo != null)
            {
                currentMissionSo.OnMissionComplete += HandleMissionSoComplete;
                currentMissionSo.OnMissionFail += HandleMissionSoFail;
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
            if (currentMissionSo != null)
            {
                currentMissionSo.UpdateMission();
            }
        }
        

        private void StartMission()
        {
            currentMissionSo = mainMissionData.GetCurrentMission();
            if (currentMissionSo != null)
            {
                currentMissionSo.SpawnPosition = listMissionPoint[mainMissionData.currentMissionIndex];
                currentMissionSo.StartMission();
            }
        }

        private void HandleMissionSoComplete()
        {
            mainMissionData.AdvanceMission();
            StartMission();
        }

        private void HandleMissionSoFail()
        {
            //TODO: Logic to handle mission failure
        }*/
    }
}