using UnityEngine;

namespace SFRemastered
{
    public class FallingState : AirBoneState
    {
        
        [SerializeField] protected SprintState _sprintState;
        [SerializeField] protected SwingState _swingState;
        [SerializeField] protected LandToIdleState _landIdleState;
        [SerializeField] protected LandNormalState _landNormalState;
        [SerializeField] protected LandToRollState _landRollState;
        protected float incomingVelocity;
        public override void EnterState()
        {
            base.EnterState();
            incomingVelocity = _blackBoard.playerMovement.GetVelocity().magnitude;
        }

        public override void ExitState()
        {
           var velo = _blackBoard.playerMovement.GetVelocity();
           _blackBoard.playerMovement.SetVelocity(velo.normalized * incomingVelocity);
            base.ExitState();
        }

        public override StateStatus UpdateState()
        {
            StateStatus baseStatus = base.UpdateState();
            if (baseStatus != StateStatus.Running)
            {
                return baseStatus;
            }

            _blackBoard.playerMovement.SetMovementDirection(_blackBoard.moveDirection);

            if (_blackBoard.playerMovement.IsGrounded())
            {
                if(_blackBoard.moveDirection.magnitude < 0.3f)
                    _fsm.ChangeState(_landIdleState);
                else if(elapsedTime > .4f)
                    _fsm.ChangeState(_landNormalState);
                else
                    _fsm.ChangeState(_sprintState);

                return StateStatus.Success;
            }

            //Swing entry point, work in progress
            if (_blackBoard.swing)
            {
                _fsm.ChangeState(_swingState);
                return StateStatus.Success;
            }
            
            return StateStatus.Running;
        }
    }
}