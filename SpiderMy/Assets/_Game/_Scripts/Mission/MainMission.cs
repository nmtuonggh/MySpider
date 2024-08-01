using System.Collections.Generic;
using UnityEngine;

namespace SFRemastered._Game._Scripts.Mission
{ 
    [CreateAssetMenu(menuName = "ScriptableObjects/Mission/MissionSystem")]
    public class MainMission : ScriptableObject
    {
        public List<BaseMission> listMission;
        public int currentMissionIndex;
    }
}