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

            if (GroundCheck())
            {   
                _fsm.ChangeState(_landRollState);
                return StateStatus.Success;
            }

            if (_blackBoard.playerMovement.GetVelocity().y < -8f )
            {
                _fsm.ChangeState(_diveState);
                return StateStatus.Success;
            }

            return StateStatus.Running;
        }

        public override void ExitState()
        {
            base.ExitState();
            _blackBoard.characterVisual.transform.DORotate(new Vector3(0, _blackBoard.playerMovement.transform.eulerAngles.y, 0), 0.2f);
        }
        
        private void RandomAnim()
        {
            _state = _blackBoard.animancer.Play(_litsAnimation[_animIndex]);
        }
        
        private bool GroundCheck()
        {
            if (Physics.Raycast(_fsm.transform.position, Vector3.down, 0.3f, _blackBoard.groundLayers))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}