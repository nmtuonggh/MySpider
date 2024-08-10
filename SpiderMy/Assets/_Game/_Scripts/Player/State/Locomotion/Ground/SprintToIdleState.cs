using System.Collections;
using System.Collections.Generic;
using SFRemastered._Game._Scripts.Player.State.Combat.Gadget;
using UnityEngine;
using UnityEngine.Serialization;

namespace SFRemastered
{
    [CreateAssetMenu(menuName = "ScriptableObjects/States/SprintToIdle")]
    public class SprintToIdleState : GroundState
    {
        [SerializeField] private IdleState _idleState;
        [SerializeField] private GadgetAdapter _gadgetAdapter;
        [SerializeField] private SprintState _sprintState;
        public override void EnterState()
        {
            base.EnterState();

            _state.Events.OnEnd = () =>
            {
                _fsm.ChangeState(_idleState);
            };


            _blackBoard.playerMovement.useRootMotion = true;
        }

        public override StateStatus UpdateState()
        {
            StateStatus baseStatus = base.UpdateState();
            if (baseStatus != StateStatus.Running)
            {
                return baseStatus;
            }

            if (_blackBoard.moveDirection.magnitude > 0.1f)
            {
                _fsm.ChangeState(_sprintState);
                return StateStatus.Success;
            }
            
            if (_blackBoard.gadget && _blackBoard.gadgetIndex != -1)
            {
                _fsm.ChangeState(_gadgetAdapter);
                return StateStatus.Success;
            }

            return StateStatus.Running;
        }

        public override void ExitState()
        {
            base.ExitState();

            _blackBoard.playerMovement.useRootMotion = false;
        }
    }
}
