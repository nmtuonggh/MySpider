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
            _blackBoard.windEffect.Stop();
        }


        public override StateStatus UpdateState()
        {
            StateStatus baseStatus = base.UpdateState();
            if (baseStatus != StateStatus.Running)
            {
                return baseStatus;
            }
            
            if (_blackBoard.playerMovement.GetVelocity().magnitude > 15)
            {
                _blackBoard.windEffect.Play();
            }
            else
            {
                DOVirtual.DelayedCall(0.2f, () => _blackBoard.windEffect.Stop());
            }
            
            ((LinearMixerState)_state).Parameter = Mathf.Lerp(((LinearMixerState)_state).Parameter, _blackBoard.moveDirection.x, 55 * Time.deltaTime);
            
            if (_blackBoard.playerMovement.IsGrounded())
            {
                _fsm.ChangeState(_landRollState);
                return StateStatus.Success;
            }
            
            if (_blackBoard.swing && _blackBoard.readyToSwing && !GroundCheck())
            {
                _fsm.ChangeState(_swingState);
                return StateStatus.Success;
            }
            
            if (_blackBoard.foundWall)
            {
                _fsm.ChangeState(_wallRun);
                return StateStatus.Success;
            }
            
            return StateStatus.Running;
        }
    }
}