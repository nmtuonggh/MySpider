using System;
using UnityEngine;

namespace SFRemastered._Game._Scripts.Mission
{
    
    public abstract class BaseMissionSO : ScriptableObject
    {
        [Header("General Information")]
        private Transform spawnPosition;
        public GameObject missionPrefab;
        public GameObject missionRangePrefab;
        public GameObject indicatorPrefab;

        [Header("Rewards")] 
        public float cashReward;
        public float expReward;
        
        public float timeLimit;
        
        public event Action OnMissionStart;
        public event Action OnMissionComplete;
        public event Action OnMissionFail;

        public Transform SpawnPosition
        {
            get => spawnPosition;
            set => spawnPosition = value;
        }


        /*public virtual void StartMission()
        {
            OnMissionStart?.Invoke();
        }
        
        public virtual void UpdateMission()
        {
            
        }
        
        public virtual void CompleteMission()
        {
            OnMissionComplete?.Invoke();
        }
        
        public virtual void FailMission()
        {
            OnMissionFail?.Invoke();
        }*/
    }
}