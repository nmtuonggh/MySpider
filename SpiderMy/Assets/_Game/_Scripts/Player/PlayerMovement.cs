using EasyCharacterMovement;
using SFRemastered.InputSystem;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace SFRemastered
{
    public class PlayerMovement : Character
    {
        public float rootmotionSpeedMult = 1;
        public bool foudWall;
        public LayerMask wallLayer;
        public Transform wallCheckPoint;
        public RaycastHit hit;
        protected override void HandleInput(){}

        protected override Vector3 CalcDesiredVelocity()
        {
            // Current movement direction

            Vector3 movementDirection = GetMovementDirection();

            // The desired velocity from animation (if using root motion) or from input movement vector

            Vector3 desiredVelocity = useRootMotion && rootMotionController
                ? rootMotionController.ConsumeRootMotionVelocity(deltaTime) * rootmotionSpeedMult
                : movementDirection * GetMaxSpeed();

            // Return desired velocity (constrained to constraint plane if any)

            return characterMovement.ConstrainVectorToPlane(desiredVelocity);
        }

        private void CheckWallState()
        {
            //foudWall = Physics.Raycast(wallCheckPoint.position, transform.forward, 1f, wallLayer);
            Debug.DrawRay(wallCheckPoint.position, transform.forward * 1f, Color.red);
            foudWall = Physics.Raycast(transform.position, transform.forward, out hit, 1f, wallLayer);
        }
        protected override void Update()
        {
            base.Update();
            CheckWallState();
        }
    }
}
