using System;
using _Game.Scripts.Event;
using UnityEditor;
using UnityEngine;

namespace SFRemastered._Game._Scripts.Mission
{
    public class BaseMission : MonoBehaviour
    {
        [Header("Game Events")]
        public GameEvent onMissionStart;
        public GameEvent onMissionComplete;
        public GameEvent onMissionFail;
        public GameEvent onMissionUpdate;
        public GameEvent onMissionFailByDie;
        
        [Header("Mission Status")]
        public bool progressing = false;

        private void OnValidate()
        {
            #if UNITY_EDITOR
            
            onMissionStart = AssetDatabase.LoadAssetAtPath<GameEvent>("Assets/_Game/ScriptableObjects/EventSO/MissionEvent/OnMissionStart.asset");
            onMissionComplete = AssetDatabase.LoadAssetAtPath<GameEvent>("Assets/_Game/ScriptableObjects/EventSO/MissionEvent/OnMissionComplete.asset");
            onMissionFail = AssetDatabase.LoadAssetAtPath<GameEvent>("Assets/_Game/ScriptableObjects/EventSO/MissionEvent/OnMissionFail.asset");
            onMissionUpdate = AssetDatabase.LoadAssetAtPath<GameEvent>("Assets/_Game/ScriptableObjects/EventSO/MissionEvent/OnMissionUpdate.asset");
            
            #endif
        }

        public virtual void StartMission()
        {
            onMissionStart.Raise();
        }

        public virtual void UpdateMission()
        {
            onMissionUpdate.Raise();
        }

        public virtual void CompleteMission()
        {
            onMissionComplete.Raise();
        }
        
        public virtual void FailMission()
        {
            onMissionFail.Raise();
        }
        
        public virtual void FailMissionByDie()
        {
            onMissionFailByDie.Raise();
        }

        
    }
}