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

        private void OnValidate()
        {
            #if UNITY_EDITOR
            
            OnPlayerInRange = UnityEditor.AssetDatabase.LoadAssetAtPath<GameEvent>("Assets/_Game/ScriptableObjects/EventSO/PlayerInRangeMission.asset");
            OnPlayerOutOfRange = UnityEditor.AssetDatabase.LoadAssetAtPath<GameEvent>("Assets/_Game/ScriptableObjects/EventSO/PlayerOutRangeMission.asset");
            
            #endif
        }

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
        
        //draw the sphere in the editor
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, radius);
        }
        
        #region TriggerButFail

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

        #endregion
    }
}