using System;
using Animancer;
using NodeCanvas.Framework;
using SFRemastered._Game._Scripts.Enemy.Bullet;
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
        public LineRenderer lineRenderer;
        public GameObject healthBarUI;
        
        [Header("============Actions bool============")]
        public bool attacking;
        public bool zipAttackStun;
        public bool staggerHit;
        public bool stunLockHit;
        public bool knockBackHit;
        public bool die;
        
        [Header("============Combat variables============")]
        public float webHitStun;
        public float stunLockTime;
        public GameObject sphereCastCenter;
        public LayerMask hitLayer;
        public GameObject warningAttack;
        public GameObject bulletHolder;
        
        [Header("============Range variables==========")]
        public BulletSO bulletSo;
        public GameObject shootPosition;

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