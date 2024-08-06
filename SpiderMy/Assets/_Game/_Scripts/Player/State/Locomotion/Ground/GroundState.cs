using System.Collections;
using System.Collections.Generic;
using SFRemastered._Game._Scripts.State.Combat;
using SFRemastered._Game._Scripts.State.Locomotion.Ground;
using SFRemastered.Combat;
using UnityEngine;
using UnityEngine.Serialization;

namespace SFRemastered
{
    public abstract class GroundState : StateBase
    {
        [SerializeField] protected JumpState _jumpState;
        [SerializeField] protected FallState _fallState;
        [SerializeField] protected ZipState _zipState;
        [FormerlySerializedAs("_attackController")] [SerializeField] protected CombatController combatController;
        [SerializeField] protected DodgeState _dodgeState;
        [SerializeField] protected UltimateSkill _ultimateSkill;

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
                _fsm.ChangeState(combatController);
                return StateStatus.Success;
            }

            if (_blackBoard.dodge)
            {
                _fsm.ChangeState(_dodgeState);
                return StateStatus.Success;
            }
            
            if (_blackBoard.ultimate)
            {
                _fsm.ChangeState(_ultimateSkill);
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