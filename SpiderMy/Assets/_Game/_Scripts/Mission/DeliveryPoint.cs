using System;
using _Game.Scripts.Event;
using UnityEngine;

namespace SFRemastered._Game._Scripts.Mission
{
    public class DeliveryPoint : MonoBehaviour
    {
        public GameEvent onDeliveryPointReached;
        public GameEvent OnPlayerOutOfRange;
        public LayerMask layer;
        public float radius;
        public Collider[] hitColliders;
        private void Update()
        {
            hitColliders = Physics.OverlapSphere(transform.position,radius, layer);

            if (hitColliders.Length > 0)
            {
                onDeliveryPointReached.Raise();
                Destroy(this.gameObject);
            }
            /*else
            {
                OnPlayerOutOfRange.Raise();
            }*/
        }
    }
}