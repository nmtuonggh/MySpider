using Animancer;
using DG.Tweening;
using UnityEngine;

namespace SFRemastered
{
    [CreateAssetMenu(menuName = "ScriptableObjects/States/JumpFromSwing")]
    public class JumpFromSwing : AirBoneState
    {
        [SerializeField] private DiveState _diveState;
        [SerializeField] private LandRollState _landRollState;
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
            _blackBoard.characterVisual.transform.DORotate(new Vector3(0, _blackBoard.playerMovement.transform.eulerAngles.y, 0), 0.2f);
            var valueForward = _blackBoard.playerMovement.transform.forward.normalized;
            var forceDirecton = _blackBoard.playerMovement.GetVelocity().normalized;
            var veloValue = _blackBoard.playerMovement.GetVelocity().magnitude;
            var totalForce = forceDirecton * veloValue + valueForward * forceValue;
            _blackBoard.playerMovement.AddForce(totalForce, ForceMode.Impulse);
            _animIndex = Random.Range(0, _animCount);
            RandomAnim();
            _state.Events.OnEnd = () => _fsm.ChangeState(_diveState);
        }

        public override StateStatus UpdateState()
        {
            StateStatus baseStatus = base.UpdateState();
            if (baseStatus != StateStatus.Running)
            {
                return baseStatus;
            }
            
            if (_blackBoard.playerMovement.GetVelocity().magnitude > 15)
            {
                _blackBoard.windEffect.Play();
            }
            else
            {
                DOVirtual.DelayedCall(0.2f, () => _blackBoard.windEffect.Stop());
            }

            if (_blackBoard.playerMovement.IsGrounded())
            {   
                _fsm.ChangeState(_landRollState);
                return StateStatus.Success;
            }

            if (_blackBoard.playerMovement.GetVelocity().y < -5f )
            {
                _fsm.ChangeState(_diveState);
                return StateStatus.Success;
            }

            return StateStatus.Running;
        }

        public override void ExitState()
        {
            base.ExitState();
            _blackBoard.windEffect.Stop();
            _blackBoard.characterVisual.transform.DORotate(new Vector3(0, _blackBoard.playerMovement.transform.eulerAngles.y, 0), 0.2f);
        }
        
        private void RandomAnim()
        {
            _state = _blackBoard.animancer.Play(_litsAnimation[_animIndex]);
        }
        
    }
}