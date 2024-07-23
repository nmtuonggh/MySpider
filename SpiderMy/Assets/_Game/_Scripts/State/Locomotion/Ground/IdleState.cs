 using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace SFRemastered
{
    [CreateAssetMenu(menuName = "ScriptableObjects/States/Idle")]
    public class IdleState : GroundState
    {
        [SerializeField] private SprintState _sprintState;

        public override void EnterState()
        {
            base.EnterState();

            _blackBoard.playerMovement.SetMovementDirection(Vector3.zero);
        }

        public override StateStatus UpdateState()
        {
            StateStatus baseStatus = base.UpdateState();
            if(baseStatus != StateStatus.Running)
            {
                return baseStatus;
            }

            if(_blackBoard.moveDirection.magnitude > 0f)
            {
                _fsm.ChangeState(_sprintState);
                return StateStatus.Success;
            }

            return StateStatus.Running;
        }
    }
}