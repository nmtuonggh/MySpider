using Animancer;
using NodeCanvas.Framework;
using UnityEngine;

namespace SFRemastered._Game._Scripts.Enemy.State
{
    [System.Serializable]
    public class EnemyBlackBoard : MonoBehaviour
    {
        public AnimancerComponent animancer;
        public CharacterController characterController;
        public Transform target;
    }
}