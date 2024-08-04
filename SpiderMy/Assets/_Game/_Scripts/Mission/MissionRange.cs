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
        public LayerMask layer;
        public float radius;
        public Collider[] hitColliders;
        /*private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                playerInRange = true;
                OnPlayerInRange.Raise();
                Debug.Log("Trigge");
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                playerInRange = false;
                OnPlayerOutOfRange.Raise();
            }
        }*/

        private void Update()
        {
             hitColliders = Physics.OverlapSphere(transform.position,radius, layer);

            if (hitColliders.Length > 0)
            {
                playerInRange = true;
                OnPlayerInRange.Raise();
            }
            else
            {
                playerInRange = false;
                OnPlayerOutOfRange.Raise();
            }
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}