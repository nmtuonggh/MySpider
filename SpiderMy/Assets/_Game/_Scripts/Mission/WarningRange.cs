using System;
using _Game.Scripts.Event;
using UnityEngine;

namespace SFRemastered._Game._Scripts.Mission
{
    public class WarningRange : MonoBehaviour
    {
        public GameEvent outWarningRange;
        public GameEvent inWarningRange;
        public bool playerInRange;
        public float radius = 40f;
        public LayerMask LayerMask ;
        
        private bool previousPlayerInRange;

        private void Start()
        {
            previousPlayerInRange = false;
        }

        private void Update()
        {
            var hitColliders = Physics.OverlapSphere(transform.position, radius, LayerMask);
            playerInRange = hitColliders.Length > 0;

            if (playerInRange && !previousPlayerInRange)
            {
                //Debug.Log("Player in range");
                inWarningRange.Raise();
            }
            else if (!playerInRange && previousPlayerInRange)
            {
                //Debug.Log("Player out of range");
                outWarningRange.Raise();
            }

            previousPlayerInRange = playerInRange;
        }
        /*private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Player in warningRange Mission");
                inWarningRange.Raise();
            }
            
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Player out warningRange Mission");

                outWarningRange.Raise();
            }
        }*/
    }
}