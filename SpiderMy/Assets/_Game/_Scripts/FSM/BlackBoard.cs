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
        public Vector3 moveDirection;
        public bool jump;
        public bool sprint;
        public bool swing;
        public bool isGrounded;
        public LayerMask groundLayers;
        public Transform swingPoint;
        public Transform playerSwingPoint;
        public Transform playerPoint;
        public LineRenderer lr;
        
    }
}
