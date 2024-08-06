using Animancer;
using DG.Tweening;
using UnityEngine;

namespace SFRemastered
{
    [CreateAssetMenu(menuName = "ScriptableObjects/States/Dive")]
    public class DiveState : FallingState
    {
        [SerializeField] private LandRollState _landRollState;
        [SerializeField] private LinearMixerTransition _diveBlendTree;
        
        public override void EnterState()
        {
            base.EnterState();
            _state = _blackBoard.animancer.Play(_diveBlendTree);
        }

        public override void ExitState()
        {
            base.ExitState();
        }


        public override StateStatus UpdateState()
        {
            StateStatus baseStatus = base.UpdateState();
            if (baseStatus != StateStatus.Running)
            {
                return baseStatus;
            }
            
            ((LinearMixerState)_state).Parameter = Mathf.Lerp(((LinearMixerState)_state).Parameter, _blackBoard.moveDirection.x, 55 * Time.deltaTime);
            
            if (_blackBoard.playerMovement.IsGrounded())
            {
                _fsm.ChangeState(_landRollState);
                return StateStatus.Success;
            }
            
            return StateStatus.Running;
        }
    }
}