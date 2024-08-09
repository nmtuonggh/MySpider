using Animancer;
using NodeCanvas.Framework;
using SFRemastered._Game._Scripts.ReferentSO;
using UnityEngine;

namespace SFRemastered._Game._Scripts.Enemy.State
{
    [System.Serializable]
    public class EnemyBlackBoard : MonoBehaviour
    {
        public AnimancerComponent animancer;
        public CharacterController characterController;
        public GameObjectRef target;
        
        [Header("Actions bools")]
        public bool attack;
        public bool zipAttackStun;
        public bool staggerHit;
        public bool stunLockHit;
        
        public float webHitStun;
        
        public float stunLockTime;
    }
}