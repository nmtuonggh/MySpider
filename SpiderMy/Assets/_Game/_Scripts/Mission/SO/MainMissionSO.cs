using System.Collections.Generic;
using UnityEngine;

namespace SFRemastered._Game._Scripts.Mission
{ 
    [CreateAssetMenu(menuName = "ScriptableObjects/Mission/MainMissionSO")]
    public class MainMissionSO : ScriptableObject
    {
        public List<BaseMission> listMission;
        public int currentMissionIndex;

        public BaseMission GetCurrentMission()
        {
            if (currentMissionIndex < listMission.Count)
            {
                return listMission[currentMissionIndex];
            }
            return null;
        }

        public void AdvanceMission()
        {
            currentMissionIndex++;
        }
    }
}