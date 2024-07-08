using Animancer;
using System.Collections;
using System.Collections.Generic;
using SFRemastered._Game._Scripts.FSM.Swing;
using UnityEngine;

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
        public SwingPoint swingPoint;
        public Transform swingPoint2;
        public Transform playerPoint;
        public LineRenderer lr;
    }
}
