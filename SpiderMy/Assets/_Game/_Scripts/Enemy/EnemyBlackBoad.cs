﻿using System;
using Animancer;
using NodeCanvas.Framework;
using SFRemastered._Game._Scripts.ReferentSO;
using UnityEngine;

namespace SFRemastered._Game._Scripts.Enemy.State
{
    [System.Serializable]
    public class EnemyBlackBoard : MonoBehaviour
    {
        public EnemySO enemyData;
        public AnimancerComponent animancer;
        public CharacterController characterController;
        public GameObjectRef target;
        
        [Header("Actions bools")]
        public bool attacking;
        public bool zipAttackStun;
        public bool staggerHit;
        public bool stunLockHit;
        public bool knockBackHit;
        public bool die;
        
        [Header("Combat variables")]
        public float webHitStun;
        public float stunLockTime;
        public GameObject sphereCastCenter;
        public LayerMask hitLayer;
        public GameObject warningAttack;

        public Vector3 startWanderPosition;
        public int wanderPositionIndex;

        private void OnEnable()
        {
           //set all bool to false
           attacking = false;
           zipAttackStun = false;
           staggerHit = false;
           stunLockHit = false; 
           knockBackHit = false;
           die = false;
        }
    }
}