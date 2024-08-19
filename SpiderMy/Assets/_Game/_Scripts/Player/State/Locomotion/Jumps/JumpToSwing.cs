using Animancer;
using SFRemastered.OnHitState;
using UnityEngine;

namespace SFRemastered
{
    [CreateAssetMenu(menuName = "ScriptableObjects/States/JumpToSwing")]
    public class JumpToSwing : AirBoneState
    {
        [SerializeField] private float jumpImpulseModifier = 1f;
        [SerializeField] private SprintState _sprintState;
        [SerializeField] private FallState _fallState;
        [SerializeField] private ClipTransition _fallLoopAnimation;
        [SerializeField] private StaggerState stagger;
        [SerializeField] private OnVenomHitP1 venomHit;
        [SerializeField] private OnVenomHitP2 venomHit2;
        [SerializeField] private OnVenomFinalHit venomHit3;

        public override void EnterState()
        {
            base.EnterState();
            _blackBoard.playerMovement.jumpImpulse *= jumpImpulseModifier;
            _blackBoard.playerMovement.Jump();
            _state.Events.OnEnd = () => { _state = _blackBoard.animancer.Play(_fallLoopAnimation);};
        }

        public override void ExitState()
        {
            base.ExitState();
            _blackBoard.playerMovement.StopJumping();
            _blackBoard.playerMovement.jumpImpulse /= jumpImpulseModifier;
        }


        public override StateStatus UpdateState()
        {
            StateStatus baseStatus = base.UpdateState();
            if (baseStatus != StateStatus.Running)
            {
                return baseStatus;
            }

            if (elapsedTime > .1f)
                _blackBoard.playerMovement.StopJumping();

            _blackBoard.playerMovement.SetMovementDirection(_blackBoard.moveDirection);

            if (_blackBoard.playerMovement.IsGrounded() && elapsedTime > .2f)
            {
                _fsm.ChangeState(_sprintState);
                return StateStatus.Success;
            }
            

            if (_blackBoard.playerMovement.GetVelocity().y < 0 && elapsedTime > .2f)
            {
                _fsm.ChangeState(_fallState);
                return StateStatus.Success;
            }
            
            if (_blackBoard.staggerHit)
            {
                _fsm.ChangeState(stagger);
                return StateStatus.Success;
            }
            
            if (_blackBoard.venomP1Hit)
            {
                _fsm.ChangeState(venomHit);
                return StateStatus.Success;
            }
            
            if (_blackBoard.venomP2Hit)
            {
                _fsm.ChangeState(venomHit2);
                return StateStatus.Success;
            }
            if (_blackBoard.venomFinalHit)
            {
                _fsm.ChangeState(venomHit3);
                return StateStatus.Success;
            }


            return StateStatus.Running;
        }
    }
}