using Animancer;
using UnityEngine;

namespace SFRemastered._Game.ScriptableObjects.AnimationAttack
{
    [CreateAssetMenu(menuName = "ScriptableObjects/AttackAnimation")]
    public class AttackAnim : ScriptableObject
    {
        public ClipTransition clip;
        public float damage;
        public float delayAttack;
    }
}