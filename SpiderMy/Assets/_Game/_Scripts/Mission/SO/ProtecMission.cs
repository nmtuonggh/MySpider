using System.Collections.Generic;
using UnityEngine;

namespace SFRemastered._Game._Scripts.Mission
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Mission/ProtecMission")]
    public class ProtecMissionSO : BaseMissionSO
    {
        [Header("Protec Mission")]
        public GameObject warningRange;
        public float spawnRange;
        public GameObject victim;
        
        [Header("Wave Data")]
        public int currentWaveIndex = 0;
        public int currentWaveEnemyCount = 0;
        public List<WaveCombat> listWaveCombat;
    }
}