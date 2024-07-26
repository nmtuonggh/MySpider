 using System.Collections;
 using SFRemastered._Game._Scripts.State.Combat.IdleCombat;
 using SFRemastered.Combat;
 using UnityEngine;
using UnityEngine.Serialization;

namespace SFRemastered
{
    [CreateAssetMenu(menuName = "ScriptableObjects/States/Idle")]
    public class IdleState : GroundState
    {
        [SerializeField] private SprintState _sprintState;
        [SerializeField] private JumpToSwing _jumpToSwing;
        [SerializeField] private IdleCombat _idleCombat;

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

            if (_blackBoard.swing)
            {
                _fsm.ChangeState(_jumpToSwing);
                return StateStatus.Success;
            }

            if (_blackBoard.attack)
            {
                _fsm.ChangeState(_idleCombat);
                return StateStatus.Success;
            }

            return StateStatus.Running;
        }
    }
}