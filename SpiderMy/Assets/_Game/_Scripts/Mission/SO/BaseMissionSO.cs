using System;
using SFRemastered._Game._Scripts.Player.State.Combat.Gadget;
using UnityEngine;

namespace SFRemastered._Game._Scripts.Mission
{
    public enum MissionType
    {
        Fighting,
        Delivery,
        Protect
    }
    public abstract class BaseMissionSO : ScriptableObject
    {
        [Header("General Information")]
        public MissionType missionType;
        private Transform spawnPosition;
        public GameObject missionPrefab;
        /*public GameObject missionRangePrefab;
        public GameObject indicatorPrefab;*/
        public PoolObject missionRangePrefab;
        public PoolObject indicatorPrefab;
        public float timeLimit;

        [Header("Rewards")] 
        public float cashReward;
        public float expReward;
        
        public Transform SpawnPosition
        {
            get => spawnPosition;
        }

        public virtual void GetMissionPosition(Transform mssPoint)
        {
            spawnPosition = mssPoint;
        }
    }
}