using Animancer;
using UnityEngine;

namespace SFRemastered
{
    [CreateAssetMenu(menuName = "ScriptableObjects/States/JumpFromSwing")]
    public class JumpFromSwing : StateBase
    {
        [SerializeField] private FallState _fallState;
        [SerializeField] private LandToIdleState _landIdleState;
        [SerializeField] private LandNormalState _landNormalState;
        [SerializeField] private LandToRollState _landRollState;
        [SerializeField] private SprintState _sprintState;
        [SerializeField] private ClipTransition _fallLoopAnimation;

        private Vector3 startVelocity;

        public override void EnterState()
        {
            base.EnterState();
            startVelocity = _blackBoard.playerMovement.GetVelocity();
            var forceDirecton = _blackBoard.playerMovement.GetVelocity().normalized;
            var forceValue = 30f;
            var totalForce = forceDirecton * forceValue;
            _blackBoard.playerMovement.AddForce(totalForce, ForceMode.Impulse);
            _state.Events.OnEnd = () =>
            {
                //_state = _blackBoard.animancer.Play(_fallLoopAnimation);
                //_blackBoard.playerMovement.SetVelocity(initialVelocity);
            };
        }

        public override StateStatus UpdateState()
        {
            base.UpdateState();

            if (_blackBoard.playerMovement.IsGrounded())
            {
                if (_blackBoard.moveDirection.magnitude < 0.3f)
                    _fsm.ChangeState(_landIdleState);
                else if (elapsedTime > .4f)
                    _fsm.ChangeState(_landNormalState);
                else
                    _fsm.ChangeState(_sprintState);

                return StateStatus.Success;
            }

            if (_blackBoard.playerMovement.GetVelocity().y < 0 )
            {
                _fsm.ChangeState(_fallState);
                return StateStatus.Success;
            }

            return StateStatus.Running;
        }

        public override void ExitState()
        {
            base.ExitState();
            _blackBoard.playerMovement.SetVelocity(startVelocity);
        }
    }
}