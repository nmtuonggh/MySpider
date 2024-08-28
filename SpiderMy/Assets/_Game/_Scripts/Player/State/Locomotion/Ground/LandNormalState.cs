using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Serialization;

namespace SFRemastered
{
    [CreateAssetMenu(menuName = "ScriptableObjects/States/LandNormal")]
    public class LandNormalState : GroundState
    {
        [FormerlySerializedAs("_walkState")] [SerializeField] private SprintState _sprintState;
        [SerializeField] private SprintToIdleState  _sprintToIdleState ;
        public override void EnterState()
        {
            base.EnterState();
            _blackBoard.characterVisual.transform.rotation = Quaternion.LookRotation(_blackBoard.playerMovement.transform.forward, Vector3.up);
            //_state.Events.SetShouldNotModifyReason(null);
            _state.Events.OnEnd = () =>
            {
                _fsm.ChangeState(_sprintState);
            };
        }

        public override StateStatus UpdateState()
        {
            StateStatus baseStatus = base.UpdateState();
            if (baseStatus != StateStatus.Running)
            {
                return baseStatus;
            }

            if(_blackBoard.moveDirection.magnitude > 0.1f)
                _blackBoard.playerMovement.SetMovementDirection(_blackBoard.moveDirection);
            else
                _blackBoard.playerMovement.SetMovementDirection(_fsm.transform.forward);

            if (_blackBoard.swing)
            {
                _fsm.ChangeState(_blackBoard.stateReference.JumpToSwing);
                return StateStatus.Success;
            }
            return StateStatus.Running;
        }

        public void FinishRoll()
        {
            if (_blackBoard.moveDirection.magnitude < 0.1f)
            {
                _fsm.ChangeState(_sprintToIdleState);
            }
        }
    }
}
