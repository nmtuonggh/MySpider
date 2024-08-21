using System;
using _Game.Scripts.Event;
using Animancer;
using UnityEngine;

namespace SFRemastered._Game._Scripts.Mission
{
    public class DeliveryPoint : MonoBehaviour
    {
        public GameEvent onDeliveryPointReached;
        public AnimancerComponent animancer;
        public ClipTransition idle;
        public ClipTransition eat;
        private bool playerInRange;

        private void OnEnable()
        {
            playerInRange = false;
            animancer.Play(idle);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player") && playerInRange==false)
            {
                playerInRange = true;
                onDeliveryPointReached.Raise();
                animancer.Play(eat).Events.OnEnd = () => Destroy(this.gameObject);
            }
        }
    }
}