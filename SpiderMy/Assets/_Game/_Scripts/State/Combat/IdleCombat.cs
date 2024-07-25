using UnityEngine;

namespace SFRemastered.Combat
{
    [CreateAssetMenu(menuName = "ScriptableObjects/States/CombatStates/Idle")]

    public class IdleCombat : CombatState
    {
        [SerializeField] private SprintState _sprintState;
        [SerializeField] private JumpToSwing _jumpToSwing;
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
            
            return StateStatus.Running;
        }

        public override void ExitState()
        {
            base.ExitState();
        }
    }
}