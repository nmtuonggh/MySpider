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
        public float rayCastDistance;

        public bool foudWall;
        
        public LayerMask wallLayer;
        public Transform wallCheckPoint;
        public RaycastHit hit;
        
        [Header("Detected Zip Point")]
        public new Camera camera;
        public Vector3 zipPoint;
        public RaycastHit SphereCastDetected;
        public float spcastRadius;
        public float spcastDistance;

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

        private void RaycastCheckWallState()
        {
            var forward = transform.forward;
            Debug.DrawRay(wallCheckPoint.position, forward * rayCastDistance, Color.red);
            foudWall = Physics.Raycast(transform.position, forward, out hit, rayCastDistance, wallLayer);
        }
        
        private void DetectZipPoint()
        {
            if (Physics.SphereCast(camera.transform.position, spcastRadius, camera.transform.forward, out SphereCastDetected, spcastDistance, wallLayer))
            {
                var wallScript = SphereCastDetected.transform.GetComponent<Town>();
                zipPoint = wallScript.GetZipPoint(SphereCastDetected.point);
            }
        }
        protected override void Update()
        {
            base.Update();
            RaycastCheckWallState();
            //DetectZipPoint();
        }
        
    }
}
