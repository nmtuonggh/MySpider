using DG.Tweening;
using EasyCharacterMovement;
using SFRemastered._Game._Scripts.Player.State.Combat;
using SFRemastered._Game._Scripts.State.Combat.IdleCombat;
using UnityEngine;

namespace SFRemastered.OnHitState
{
    [CreateAssetMenu(menuName = "ScriptableObjects/States/CombatStates/VenomFinalHit")]

    public class OnVenomFinalHit: StateBase
    {
        [SerializeField] private Riseup riseupState;

        public override void EnterState()
        {
            base.EnterState();
            _blackBoard.rigidbody.interpolation = RigidbodyInterpolation.None;
            _blackBoard.playerMovement.rootmotionSpeedMult = 2f;
            _blackBoard.playerMovement.useRootMotion = true;
        }

        public override StateStatus UpdateState()
        {
            StateStatus baseStatus = base.UpdateState();
            if (baseStatus != StateStatus.Running)
            {
                return baseStatus;
            }

            _blackBoard.playerMovement.SetMovementDirection(Vector3.zero);
            if (_state.NormalizedTime >= 1f)
            {
                _fsm.ChangeState(riseupState);
                return StateStatus.Success;
            }

            return StateStatus.Running;
        }

        public override void ExitState()
        {
            base.ExitState();
            _blackBoard.playerController.transform.DORotate(
                Quaternion.LookRotation(_blackBoard.playerController.transform.forward.projectedOnPlane(Vector3.up), Vector3.up).eulerAngles,
                0.2f);
            _blackBoard.rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
            _blackBoard.playerMovement.rootmotionSpeedMult = 1f;
            _blackBoard.playerMovement.useRootMotion = false;
        }
    }
}