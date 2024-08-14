using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace SFRemastered
{
    [CreateAssetMenu(menuName = "ScriptableObjects/States/Fall")]
    public class FallState : FallingState
    {
        [SerializeField] private DiveState _diveState;
        [SerializeField] protected SprintState _sprintState;
        [SerializeField] protected LandToIdleState _landIdleState;
        [SerializeField] protected LandNormalState _landNormalState;
        public override void EnterState()
        {
            base.EnterState();
        }

        public override void ExitState()
        {
            base.ExitState();
        }


        public override StateStatus UpdateState()
        {
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
            
            if (_blackBoard.swing && _blackBoard.readyToSwing)
            {
                if (_fsm.PreviousState != _swingState)
                {
                    _fsm.ChangeState(_swingState);
                }else if(_fsm.PreviousState == _swingState && elapsedTime >= 0.5f)
                    _fsm.ChangeState(_swingState);
                
                return StateStatus.Success;
            }
            
            if (_blackBoard.playerMovement.GetVelocity().y < 0 && _blackBoard.playerMovement.GetVelocity().magnitude > 40f)
            {
                _fsm.ChangeState(_diveState);
                return StateStatus.Success;
            }
            
            return StateStatus.Running;
        }
    }
}
