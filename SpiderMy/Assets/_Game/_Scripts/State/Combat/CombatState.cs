using UnityEngine;

namespace SFRemastered._Game._Scripts.State.Combat
{
    public abstract class CombatState : StateBase
    {
        [SerializeField] protected JumpState _jumpState;
        [SerializeField] protected FallState _fallState;
        [SerializeField] protected ZipState _zipState;
        [SerializeField] protected AttackController _attackController;
        [SerializeField] protected SprintState _sprintState;
        [SerializeField] protected JumpToSwing _jumpToSwing;

        public bool canJump = true;

        public override StateStatus UpdateState()
        {
            if (HandleJump())
            {
                _fsm.ChangeState(_jumpState);
                return StateStatus.Success;
            }

            if (!_blackBoard.playerMovement.IsGrounded())
            {
                _fsm.ChangeState(_fallState);
                return StateStatus.Success;
            }

            if (_blackBoard.zip && _blackBoard.findZipPoint.focusZipPointPrefab.gameObject.activeSelf)
            {
                _fsm.ChangeState(_zipState);
                return StateStatus.Success;
            }

            if (_blackBoard.attack)
            {
                _fsm.ChangeState(_attackController);
                return StateStatus.Success;
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

        protected virtual bool HandleJump()
        {
            if (canJump && _blackBoard.playerMovement.CanJump())
            {
                return _blackBoard.jump;
            }

            return false;
        }
    }
}