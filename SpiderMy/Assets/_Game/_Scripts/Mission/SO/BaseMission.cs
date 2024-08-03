using System;
using UnityEngine;

namespace SFRemastered._Game._Scripts.Mission
{
    
    public abstract class BaseMission : ScriptableObject
    {
        [Header("General Information")]
        public Transform spawnPosition;
        public GameObject missionRangePrefab;
        
        [Header("Rewards")] 
        public float cashReward;
        public float expReward;
        
        public float timeLimit;
        public GameObject indicatorPrefab;
        
        public event Action OnMissionStart;
        public event Action OnMissionComplete;
        public event Action OnMissionFail;
        
        
        public virtual void StartMission()
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
        }
    }
}