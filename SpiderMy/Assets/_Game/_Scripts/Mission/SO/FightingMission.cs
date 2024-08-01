using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace SFRemastered._Game._Scripts.Mission
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Mission/FightingMission")]
    public class FightingMission : BaseMission
    {
        public float warningRange;
        public float spawnRange;

        public List<Wave> listWave;
    }
}