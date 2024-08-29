using System.Collections.Generic;
using UnityEngine;

namespace SFRemastered._Game._Scripts.Mission
{ 
    [CreateAssetMenu(menuName = "ScriptableObjects/Mission/MainMissionSO")]
    public class MainMissionSO : ScriptableObject
    {
        public List<BaseMissionSO> listMission;
        public int currentMissionIndex;

        public BaseMissionSO GetCurrentMission()
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