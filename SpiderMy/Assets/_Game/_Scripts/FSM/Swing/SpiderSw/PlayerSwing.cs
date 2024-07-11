using UnityEngine;

namespace SFRemastered._Game._Scripts.FSM.Swing.SpiderSw
{
    public class PlayerSwing : MonoBehaviour
    {
        public Vector3 velocity;
        public float gravity = 20f;
        public Vector3 gravityDirection = new Vector3(0,1f, 0);

        public Vector3 dampingDirection;
        public float drag;
        public float maximumSpeed;
        
        public void ApplyGravity()
        {
            velocity -= gravityDirection * gravity * Time.deltaTime;
        }

        public void ApplyDamping()
        {
            dampingDirection = -velocity;
            dampingDirection *= drag;
            velocity += dampingDirection;
        }

        public void CapMaxSpeed()
        {
            velocity = Vector3.ClampMagnitude(velocity, maximumSpeed);
        }
    }
}