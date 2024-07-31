using System.Collections.Generic;
using UnityEngine;

namespace SFRemastered._Game._Scripts.Mission
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Mission/FightingMission")]
    public class FightingMission : BaseMission
    {
        public float warningRange;
        
        public List<Wave> listWave;
        
    }
}