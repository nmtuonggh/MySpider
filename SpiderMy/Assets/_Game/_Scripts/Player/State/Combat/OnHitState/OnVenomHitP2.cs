using DG.Tweening;
using SFRemastered._Game._Scripts.State.Combat.IdleCombat;
using UnityEngine;

namespace SFRemastered.OnHitState
{
    [CreateAssetMenu(menuName = "ScriptableObjects/States/CombatStates/VenomHitP2")]

    public class OnVenomHitP2: StateBase
    {
        [SerializeField] private OnVenomFinalHit finalHit;

        public override void EnterState()
        {
            base.EnterState();
            _blackBoard.rigidbody.interpolation = RigidbodyInterpolation.None;
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
                _fsm.ChangeState(finalHit);
                return StateStatus.Success;
            }

            return StateStatus.Running;
        }

        public override void ExitState()
        {
            base.ExitState();
            _blackBoard.rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
            _blackBoard.playerMovement.useRootMotion = false;
        }
    }
}