using System;
using _Game.Scripts.Event;
using UnityEngine;

namespace SFRemastered._Game._Scripts.Mission
{
    public class MissionRange : MonoBehaviour
    {
        public bool playerInRange;
        public GameEvent OnPlayerInRange;
        public GameEvent OnPlayerOutOfRange;
        
        private void OnTriggerEnter(Collider other)
        {
            playerInRange = true;
            OnPlayerInRange.Raise();
        }
        
        private void OnTriggerExit(Collider other)
        {
            playerInRange = false;
            OnPlayerOutOfRange.Raise();
        }
    }
}