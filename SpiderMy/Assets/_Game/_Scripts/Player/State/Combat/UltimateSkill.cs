using Animancer;
using SFRemastered._Game._Scripts.Enemy;
using SFRemastered._Game._Scripts.State.Combat.IdleCombat;
using SFRemastered._Game.ScriptableObjects.AnimationAttack;
using UnityEngine;

namespace SFRemastered._Game._Scripts.State.Combat
{
    [CreateAssetMenu(menuName = "ScriptableObjects/States/CombatStates/UltimateSkill")]
    public class UltimateSkill : CombatBase
    {
        [SerializeField] private AttackAnim anim;
        [SerializeField] private NormalIdleCombat _normalIdleCombat;
        [SerializeField] private LayerMask layer;
        
        public override void EnterState()
        {
            _state = _blackBoard.animancer.Play(anim.clip);
            base.EnterState();
            _blackBoard.playerMovement.useRootMotion = true;
            _blackBoard.ultimateEffectPrefab.gameObject.SetActive(true);
            _state.Events.SetShouldNotModifyReason(null);
            _state.Events.OnEnd = () =>
            {
                _fsm.ChangeState(_normalIdleCombat);
            };
        }
        
        public override StateStatus UpdateState()
        {
            
            
            
            StateStatus baseStatus = base.UpdateState();
            if(baseStatus != StateStatus.Running)
            {
                return baseStatus;
            }
            
            return StateStatus.Running;
        }
        
        public void BumChiu()
        {
            _blackBoard.ultimateEffectPrefab.Play();
        }

        public void HitDame()
        {
            _blackBoard.overlapSphereHit.UltimateHit(_blackBoard.ultimateEffectPrefab.gameObject.transform, anim.damage);
        }
        
        public override void ExitState()
        {
            base.ExitState();
            _blackBoard.ultimateEffectPrefab.gameObject.SetActive(false);
            _blackBoard.playerMovement.useRootMotion = false;
        }
    }
}