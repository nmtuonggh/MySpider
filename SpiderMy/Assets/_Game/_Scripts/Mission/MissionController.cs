using System.Collections.Generic;
using UnityEngine;

namespace SFRemastered._Game._Scripts.Mission
{
    public class MissionController : MonoBehaviour
    {
        [SerializeField] private MissionSystem missionSystem;
        
        public List<Transform> listFightingSpawnPosition;
        private void Start()
        {
            Init();
        }

        private void Init()
        {
            
        }
    }
}