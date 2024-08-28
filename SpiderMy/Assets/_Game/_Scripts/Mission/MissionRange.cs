using System;
using _Game.Scripts.Event;
using UnityEngine;

namespace SFRemastered._Game._Scripts.Mission
{
    public class MissionRange : MonoBehaviour
    {
        public bool playerInRange = false;
        public GameEvent OnPlayerInRange;
        public GameEvent OnPlayerOutOfRange;
        public GameEventListener onStartMission;
        public float radius;
        private bool previousPlayerInRange;
        public LayerMask layerMask;

        private void OnEnable()
        {
            onStartMission.OnEnable();
        }
        
        private void OnDisable()
        {
            onStartMission.OnDisable();
        }

        private void Start()
        {
            playerInRange = false;
            previousPlayerInRange = false;
        }

        private void Update()
        {
            var hitColliders = Physics.OverlapSphere(transform.position, radius, layerMask);
            playerInRange = hitColliders.Length > 0;
            
            if (playerInRange && !previousPlayerInRange)
            {
                //Debug.Log("Player in range");
                OnPlayerInRange.Raise();
            }
            else if (!playerInRange && previousPlayerInRange)
            {
                //Debug.Log("Player out of range");
                OnPlayerOutOfRange.Raise();
            }

            previousPlayerInRange = playerInRange;
        }

        public void ResetBool()
        {
            playerInRange = false;
            previousPlayerInRange = false;
        }
        
        
        #region TriggerButFail

        /*private void OnTriggerEnter(Collider other)
       {
           if (other.CompareTag("Player"))
           {
               Debug.Log("Player in range Mission " + other.name);
               OnPlayerInRange.Raise();
           }
       }

       private void OnTriggerExit(Collider other)
       {
           if (other.CompareTag("Player"))
           {
               Debug.Log("Player out range Mission " + other.name);
               OnPlayerOutOfRange.Raise();
           }
       }*/

        #endregion
    }
}