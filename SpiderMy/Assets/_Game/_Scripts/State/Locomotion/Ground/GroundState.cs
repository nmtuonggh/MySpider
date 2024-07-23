using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SFRemastered
{
    public abstract class GroundState : StateBase
    {
        [SerializeField] protected JumpState _jumpState;
        [SerializeField] protected FallState _fallState;
        [SerializeField] protected ZipState _zipState;

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

            if (_blackBoard.zip && _blackBoard.findZipPoint.zipPoint != Vector3.zero)
            {
                _fsm.ChangeState(_zipState);
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