using System.Collections.Generic;
using Animancer;
using UnityEngine;

namespace SFRemastered
{
    [CreateAssetMenu(menuName = "ScriptableObjects/States/JumpFromSwingLow")]
    public class JumpFromSwingLow : AirBoneState
    {
        [SerializeField] private FallState _fallState;
        [SerializeField] private LandToIdleState _landIdleState;
        [SerializeField] private LandNormalState _landNormalState;
        [SerializeField] private SprintState _sprintState;
        [SerializeField] private List<ClipTransition> _litsAnimation;
        
        [SerializeField] float forceValue;
        [SerializeField] private int _animCount;
        [SerializeField] private int _animIndex;

        public override void EnterState()
        {
            base.EnterState();
            var forceDirecton = _blackBoard.playerMovement.transform.forward;
            _animIndex = Random.Range(0, _animCount);
            //add force
            var totalForce = forceDirecton * forceValue;
            _blackBoard.playerMovement.AddForce(totalForce, ForceMode.Impulse);
        }

        public override StateStatus UpdateState()
        {
            StateStatus baseStatus = base.UpdateState();
            if (baseStatus != StateStatus.Running)
            {
                return baseStatus;
            }
            
            _state = _blackBoard.animancer.Play(_litsAnimation[_animIndex]);

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

            if (_blackBoard.playerMovement.GetVelocity().y < 0)
            {
                _fsm.ChangeState(_fallState);
                return StateStatus.Success;
            }

            return StateStatus.Running;
        }

        public override void ExitState()
        {
            base.ExitState();
        }
    }
}