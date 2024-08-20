using System;
using Animancer;
using System.Collections;
using System.Collections.Generic;
using SFRemastered._Game._Scripts.CastCheck.Raycast;
using SFRemastered._Game._Scripts.Player.State.Combat.Gadget;
using SFRemastered._Game._Scripts.ReferentSO;
using SFRemastered._Game._Scripts.State.Combat;
using SFRemastered._Game._Scripts.Zip;
using UnityEngine;
using UnityEngine.Serialization;

namespace SFRemastered
{
    public class BlackBoard : MonoBehaviour
    {
        public StateReference stateReference;
        public PlayerMovement playerMovement;
        public PlayerController playerController;
        public CheckWallState checkWallState;
        public FindZipPoint findZipPoint;
        
        public AnimancerComponent animancer;
        public SFXManager sfxManager;
        public new Camera camera;
        public new Rigidbody rigidbody;
        public GameObject poolManager;
        public GameObject characterVisual;
        
        public Vector3 moveDirection;
        public Vector3 wallMoveDirection;
            
        
        [Header("=====Actions bool==========================")]
        public bool jump;
        public bool sprint;
        public bool swing;
        public bool zip;
        public bool foundWall;
        public bool attack;
        public bool dodge;
        public bool ultimate;
        public bool gadget;
        public bool isGrounded;
        
        [Header("=====Swing================================")]
        public LayerMask groundLayers;
        [FormerlySerializedAs("playerHand")] public Transform playerSwingPos;
        public Transform swingPoint;
        public Transform startrope;
        public GameObject ropHolderMid;
        public GameObject ropHolderLeft;
        public GameObject ropHolderRight;
        public GameObject ropHolder;
        public LineRenderer lr;
        public bool readyToSwing = true;
        
        [Header("=====Zip================================")]
        public Transform startZipLeft;
        public Transform startZipRight;
        public RaycastCheckWall raycastCheckWall;
        
        [Header("=====Combat================================")]
        public OverlapSphereHit overlapSphereHit;
        public Transform _zipAttackHandPositon;
        public EnemyInRange enemyInRange;
        public ParticleSystem ultimateEffectPrefab;
        public SpiderSen spiderSen;
        public bool _detectedEnemy;
        public bool staggerHit;
        public bool knockBackHit;
        public bool venomP1Hit;
        public bool venomP2Hit;
        public bool venomFinalHit;
        [FormerlySerializedAs("dodging")] public bool invincible;
        
        
        [Header("Gadget")]
        public int gadgetIndex;
        public HealingBotSO healingBotSO;
        public GameObject healingBot;
        public ProjectileWebShooterSO projectileWebShooterSo;
        public Transform projectileHealingBotPosition;
        public GadgetAdapter gadgetAdapter;
        
        
        [Header("=====GameObject References================")]
        public GameObjectRef playerRef;

        private void OnEnable()
        {
            readyToSwing = true;
        }

        private void Awake()
        {
            playerRef.obj = this.gameObject;
            gadgetIndex = gadgetAdapter.gadgetIndex;
        }

        public void SetSuitData(SuitData suitData)
        {
            suitData.ApplySkin(this);
            animancer.Animator = suitData.gameObject.GetComponent<Animator>();
        }
    }
}
