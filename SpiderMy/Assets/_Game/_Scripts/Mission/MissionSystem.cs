using System;
using System.Collections.Generic;
using UnityEngine;

namespace SFRemastered._Game._Scripts.Mission
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Mission/MissionSystem")]

    public class MissionSystem : ScriptableObject
    {
        public List<BaseMission> listMission;
        public int currentMissionIndex;
        
        public void NextMission()
        {
            currentMissionIndex++;
        }
        
        public BaseMission GetCurrentMission()
        {
            return listMission[currentMissionIndex];
        }
        
        
    }
}