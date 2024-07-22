using System;
using Animancer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace SFRemastered
{
    public class BlackBoard : MonoBehaviour
    {
        public PlayerMovement playerMovement;
        public CameraController cameraController;
        public AnimancerComponent animancer;
        public SFXManager sfxManager;
        public new Camera camera;
        public new Rigidbody rigidbody;
        public GameObject characterVisual;
        
        public Vector3 moveDirection;
        public Vector3 wallMoveDirection;
        public bool jump;
        public bool sprint;
        public bool swing;
        public bool zip;
        public bool isGrounded;
        
        public LayerMask groundLayers;
        [FormerlySerializedAs("playerHand")] public Transform playerSwingPos;
        public Transform swingPoint;
        public Transform startrope;
        public GameObject ropHolder;
        public LineRenderer lr;

        private void OnDrawGizmos()
        {
            Gizmos.DrawSphere(playerMovement.SphereCastDetected.point, playerMovement.spcastRadius);
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(playerMovement.zipPoint, playerMovement.spcastRadius);
        }
    }
}
