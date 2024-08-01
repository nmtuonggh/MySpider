using UnityEngine;

namespace SFRemastered._Game._Scripts.Mission
{
    
    public class BaseMission : ScriptableObject
    {
        [Header("General Information")]
        public Transform spawnPosition;
        public float missionRange;
        
        [Header("Rewards")] 
        public float cashReward;
        public float expReward;
        
        public float timeLimit;
    }
}