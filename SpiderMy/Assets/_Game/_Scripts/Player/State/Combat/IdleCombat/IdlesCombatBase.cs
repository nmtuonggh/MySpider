using SFRemastered._Game._Scripts.Player.State.Combat.Gadget;
using SFRemastered._Game._Scripts.State.Locomotion.Ground;
using SFRemastered.OnHitState;
using UnityEngine;
using UnityEngine.Serialization;

namespace SFRemastered._Game._Scripts.State.Combat
{
    public abstract class IdlesCombatBase : StateBase
    {
        [SerializeField] protected JumpState _jumpState;
        [SerializeField] protected FallState _fallState;
        [SerializeField] protected ZipState _zipState;
        [SerializeField] protected CombatController combatController;
        [SerializeField] protected SprintState _sprintState;
        [SerializeField] protected JumpToSwing _jumpToSwing;
        [SerializeField] protected DodgeState _dodgeState;
        [SerializeField] protected UltimateSkill _ultimateSkill;
        [SerializeField] protected GadgetAdapter _gadgetAdapter;
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
            
            if (_blackBoard.gadget && _blackBoard.gadgetIndex != -1)
            {
                _fsm.ChangeState(_gadgetAdapter);
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