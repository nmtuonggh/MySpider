using Animancer;
using UnityEngine;

namespace SFRemastered
{
    [CreateAssetMenu(menuName = "ScriptableObjects/States/JumpFromSwing")]
    public class JumpFromSwing : AirBoneState
    {
        [SerializeField] private DiveState _diveState;
        [SerializeField] private LandToIdleState _landIdleState;
        [SerializeField] private LandNormalState _landNormalState;
        [SerializeField] private LandToRollState _landRollState;
        [SerializeField] private SprintState _sprintState;
        [SerializeField] private LinearMixerTransition _jumpFromSwingBlendTree;
        [SerializeField] private ClipTransition[] _litsAnimation;
        [SerializeField] private int _animCount;
        [SerializeField] private int _animIndex;

        private Vector3 startVelocity;
        private bool endAnimation;

        public override void EnterState()
        {
            base.EnterState();
            //startVelocity = _blackBoard.playerMovement.GetVelocity();
            var forceDirecton = _blackBoard.playerMovement.GetVelocity().normalized;
            //add force
            var forceValue = 30f;
            var totalForce = forceDirecton * forceValue;
            _blackBoard.playerMovement.AddForce(totalForce, ForceMode.Impulse);

            //RandomAnim();
            _animIndex = Random.Range(0, _animCount);
            /*_state.Events.OnEnd = () =>
            {
                _fsm.ChangeState(_diveState);
            };*/
        }

        public override StateStatus UpdateState()
        {
            StateStatus baseStatus = base.UpdateState();
            if (baseStatus != StateStatus.Running)
            {
                return baseStatus;
            }
            
            //_blackBoard.playerMovement.RotateTowardsWithSlerp(_blackBoard.rigidbody.velocity.normalized,  false);
            RandomAnim();

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
                _fsm.ChangeState(_diveState);
                return StateStatus.Success;
            }

            return StateStatus.Running;
        }

        public override void ExitState()
        {
            base.ExitState();
            //endAnimation = false;
            //_blackBoard.playerMovement.SetVelocity(startVelocity);
        }
        
        private void RandomAnim()
        {
            /*_state = _blackBoard.animancer.Play(_jumpFromSwingBlendTree);
            var index = Random.Range(0, _animCount);
            ((LinearMixerState)_state).Parameter = index;*/
            _state = _blackBoard.animancer.Play(_litsAnimation[_animIndex]);
        }
    }
}