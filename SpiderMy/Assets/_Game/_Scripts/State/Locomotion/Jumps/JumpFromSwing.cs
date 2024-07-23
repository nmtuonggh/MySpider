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
        [SerializeField] private SprintState _sprintState;
        [SerializeField] private LinearMixerTransition _jumpFromSwingBlendTree;
        [SerializeField] private ClipTransition[] _litsAnimation;
        [SerializeField] private int _animCount;
        [SerializeField] private int _animIndex;
        [SerializeField] private float forceValue;

        private Vector3 startVelocity;
        private bool endAnimation;

        public override void EnterState()
        {
            base.EnterState();
            var forceDirecton = _blackBoard.playerMovement.GetVelocity().normalized;
            //add force
            var totalForce = forceDirecton * forceValue;
            _blackBoard.playerMovement.AddForce(totalForce, ForceMode.Impulse);
            _animIndex = Random.Range(0, _animCount);
        }

        public override StateStatus UpdateState()
        {
            StateStatus baseStatus = base.UpdateState();
            if (baseStatus != StateStatus.Running)
            {
                return baseStatus;
            }
            
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
        }
        
        private void RandomAnim()
        {
            _state = _blackBoard.animancer.Play(_litsAnimation[_animIndex]);
        }
    }
}