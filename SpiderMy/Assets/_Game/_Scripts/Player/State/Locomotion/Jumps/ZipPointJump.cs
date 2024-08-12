using Animancer;
using DG.Tweening;
using EasyCharacterMovement;
using UnityEngine;
using UnityEngine.Serialization;

namespace SFRemastered
{
    [CreateAssetMenu(menuName = "ScriptableObjects/States/ZipPointJump")]

    public class ZipPointJump : StateBase
    {
        [FormerlySerializedAs("_fallState")] [SerializeField] private DiveState _diveState;
        [SerializeField] private EndPointLaunch _endPointLaunch;
        [SerializeField] private float _forceValue ;
        [SerializeField] private ClipTransition _endAnimation ;
        public override void EnterState()
        {
            base.EnterState();
            _blackBoard.playerMovement.SetMovementMode(MovementMode.None);
            _blackBoard.rigidbody.useGravity = true;
            _blackBoard.rigidbody.isKinematic = false;
            _blackBoard.rigidbody.AddForce( _blackBoard.playerMovement.transform.up.normalized * _forceValue, ForceMode.Impulse);
            _blackBoard.rigidbody.AddForce( _blackBoard.playerMovement.transform.forward.normalized * _forceValue, ForceMode.Impulse);
            _state.Events.OnEnd = () => 
            {
                //_fsm.ChangeState(_endPointLaunch);
                _state = _blackBoard.animancer.Play(_endAnimation);
            };
        }

        public override StateStatus UpdateState()
        {
            base.UpdateState();
            
            if (_blackBoard.rigidbody.velocity.y <= -5f)
            {
                _fsm.ChangeState(_diveState);
                return StateStatus.Success;
            }
            
            return StateStatus.Running;
        }
        
        public override void ExitState()
        {
            base.ExitState();
            _fsm.transform.DORotate(
                Quaternion.LookRotation(_fsm.transform.forward.projectedOnPlane(Vector3.up), Vector3.up).eulerAngles,
                0.2f);
            _blackBoard.playerMovement.SetMovementMode(MovementMode.Walking);
            _blackBoard.rigidbody.useGravity = false;
            _blackBoard.rigidbody.isKinematic = true;
            _blackBoard.rigidbody.constraints = RigidbodyConstraints.None;
        }
    }
}