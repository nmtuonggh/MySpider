using System;
using UnityEngine;

namespace SFRemastered._Game._Scripts.FSM.Swing.SpiderSw
{
    public class SwingSpider : MonoBehaviour
    {
        public float speed = 6.0F;
        public float gravity = 20.0F;
        private Vector3 moveDirection = Vector3.zero;
        public Camera cam;
        public SwingMechanic SwingMechanic;
        Vector3 previousPosition;

        private void Start()
        {
            previousPosition = transform.position;
        }

        public void Swinging()
        {
            transform.position = SwingMechanic.SwingMove(transform.position, previousPosition, Time.deltaTime);
            previousPosition = transform.position;
        }
    }
}