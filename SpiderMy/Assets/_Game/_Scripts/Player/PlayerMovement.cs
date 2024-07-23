using EasyCharacterMovement;
using SFRemastered.InputSystem;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace SFRemastered
{
    public class PlayerMovement : Character
    {
        public float rootmotionSpeedMult = 1;
        public LayerMask wallLayer;
        
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
    }
}
