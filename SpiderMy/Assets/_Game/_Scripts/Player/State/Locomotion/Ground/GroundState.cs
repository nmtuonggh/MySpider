using System.Collections;
using System.Collections.Generic;
using SFRemastered._Game._Scripts.Player.State.Combat.Gadget;
using SFRemastered._Game._Scripts.State.Combat;
using SFRemastered._Game._Scripts.State.Locomotion.Ground;
using SFRemastered.Combat;
using SFRemastered.OnHitState;
using UnityEngine;
using UnityEngine.Serialization;

namespace SFRemastered
{
    public abstract class GroundState : StateBase
    {
        [SerializeField] protected JumpState _jumpState;
        [SerializeField] protected FallState _fallState;
        [FormerlySerializedAs("_attackController")] [SerializeField] protected CombatController combatController;
        [SerializeField] protected DodgeState _dodgeState;
        [SerializeField] protected StaggerState stagger;
        [SerializeField] protected KnockBackState knockBack;
        [SerializeField] protected OnVenomHitP1 venomHit;
        [SerializeField] protected OnVenomHitP2 venomHit2;
        [SerializeField] protected OnVenomFinalHit venomHit3;

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

            if (_blackBoard.zip && _blackBoard.raycastCheckWall.zipPoint != Vector3.zero)
            {
                _fsm.ChangeState(_blackBoard.stateReference.StartZip);
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

            if (_blackBoard.staggerHit)
            {
                _fsm.ChangeState(stagger);
                return StateStatus.Success;
            }
            
            if (_blackBoard.knockBackHit)
            {
                _fsm.ChangeState(knockBack);
                return StateStatus.Success;
            }
            if (_blackBoard.venomP1Hit)
            {
                _fsm.ChangeState(venomHit);
                return StateStatus.Success;
            }
            if (_blackBoard.venomP2Hit)
            {
                _fsm.ChangeState(venomHit2);
                return StateStatus.Success;
            }
            if (_blackBoard.venomFinalHit)
            {
                _fsm.ChangeState(venomHit3);
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