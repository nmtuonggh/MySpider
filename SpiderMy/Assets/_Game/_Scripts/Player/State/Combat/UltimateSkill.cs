using Animancer;
using SFRemastered._Game._Scripts.State.Combat.IdleCombat;
using UnityEngine;

namespace SFRemastered._Game._Scripts.State.Combat
{
    [CreateAssetMenu(menuName = "ScriptableObjects/States/CombatStates/UltimateSkill")]
    public class UltimateSkill : CombatBase
    {
        [SerializeField] private ClipTransitionAsset _anim;

        [SerializeField] private ParticleSystem _ultimateSkillEffect;
        [SerializeField] private NormalIdleCombat _normalIdleCombat;
        private ParticleSystem _effect;
        
        public override void EnterState()
        {
            base.EnterState();
            _blackBoard.playerMovement.useRootMotion = true;
            
            _effect = Instantiate(_ultimateSkillEffect, _blackBoard.playerMovement.transform);
            _state = _blackBoard.animancer.Play(_anim);
            //BumChiu();
            _state.Events.SetShouldNotModifyReason(null);
            _state.Events.SetCallback("BumChiu", BumChiu);
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
        
        private void BumChiu()
        {
            //increase speed of effect
            
            _effect.Play();
        }
        
        public override void ExitState()
        {
            base.ExitState();
            Destroy(_effect);
            _blackBoard.playerMovement.useRootMotion = false;
        }
    }
}