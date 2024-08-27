using System.Collections.Generic;
using SFRemastered._Game._Scripts.Player.State.Combat.Gadget;
using UnityEngine;

namespace SFRemastered._Game._Scripts.Mission
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Mission/ProtecMission")]
    public class ProtecMissionSO : BaseMissionSO
    {
        [Header("Protec Mission")]
        public PoolObject warningRange;
        public float spawnRange;
        public PoolObject victim;
        
        [Header("Wave Data")]
        public int currentWaveIndex = 0;
        public int currentWaveEnemyCount = 0;
        public List<WaveCombat> listWaveCombat;
    }
}