using System;
using System.Collections.Generic;
using _Game.Scripts.Event;
using SFRemastered._Game._Scripts.Enemy;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace SFRemastered._Game._Scripts.Mission
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Mission/FightingMission")]
    public class FightingMissionSo : BaseMissionSO
    {
        [Header("Fight Mission")]
        public GameObject warningRange;
        public float spawnRange;
        
        [Header("Wave Data")]
        public int currentWaveIndex = 0;
        public int currentWaveEnemyCount = 0;
        public List<WaveCombat> listWaveCombat;
    }
}