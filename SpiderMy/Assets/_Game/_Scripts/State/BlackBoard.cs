using System;
using Animancer;
using System.Collections;
using System.Collections.Generic;
using SFRemastered._Game._Scripts.CastCheck.Raycast;
using UnityEngine;
using UnityEngine.Serialization;

namespace SFRemastered
{
    public class BlackBoard : MonoBehaviour
    {
        public PlayerMovement playerMovement;
        public CameraController cameraController;
        public CheckWallState checkWallState;
        public FindZipPoint findZipPoint;
        
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
        public bool foundWall;
        public bool attack;
        public bool isGrounded;
        
        [Header("Swing")]
        public LayerMask groundLayers;
        [FormerlySerializedAs("playerHand")] public Transform playerSwingPos;
        public Transform swingPoint;
        public Transform startrope;
        public GameObject ropHolder;
        public LineRenderer lr;
        [Header("Zip")]
        public Transform startZipLeft;
        public Transform startZipRight;
        [Header("Combat")]
        public GameObject _targetEnemy;
        public float _distanceToTargetEnemy;
        public bool _detectedEnemy;
    }
}
