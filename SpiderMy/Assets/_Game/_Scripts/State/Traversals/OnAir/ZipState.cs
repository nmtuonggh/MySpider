using Animancer;
using DG.Tweening;
using EasyCharacterMovement;
using UnityEngine;

namespace SFRemastered
{
    [CreateAssetMenu(menuName = "ScriptableObjects/States/Zip")]
    public class ZipState : StateBase
    {
        [SerializeField] private ClipTransition idleZip;
        [SerializeField] private SprintState _sprintState;
        [SerializeField] private float durationValue;
        [SerializeField] private ClipTransition _startZip;
        [SerializeField] private ClipTransition _zipping;
        [SerializeField] private ClipTransition _endZip;

        public override void EnterState()
        {
            base.EnterState();
            _blackBoard.playerMovement.SetMovementMode(MovementMode.None);
            _blackBoard.rigidbody.useGravity = false;
            _blackBoard.rigidbody.isKinematic = false;
            _blackBoard.rigidbody.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
            
            var distance = Vector3.Distance(_blackBoard.playerMovement.transform.position, _blackBoard.findZipPoint.zipPoint);
            float moveDuration = distance / durationValue;
            _blackBoard.playerMovement.transform.DOLookAt(_blackBoard.findZipPoint.zipPoint, 0.2f, AxisConstraint.Y);
            
            Sequence seq = DOTween.Sequence();
            seq.Append(_blackBoard.playerMovement.transform.DOMove(_blackBoard.findZipPoint.zipPoint +  new Vector3(0,0.9f,0), moveDuration));
            seq.InsertCallback(moveDuration * 0.05f, () => _state = _blackBoard.animancer.Play(_startZip));
            seq.InsertCallback(moveDuration * 0.2f, () => _state = _blackBoard.animancer.Play(_zipping)); 
            seq.InsertCallback(moveDuration * 0.9f, () => _state = _blackBoard.animancer.Play(_endZip)); 
            //seq.InsertCallback(moveDuration * 1.0f, () => _state = _blackBoard.animancer.Play(_startZip)); 
        }

        public override StateStatus UpdateState()
        {
            StateStatus baseStatus = base.UpdateState();
            if (baseStatus != StateStatus.Running)
            {
                return baseStatus;
            }
            
            /*_state = _blackBoard.animancer.Play(_zipBlendTree);
            ((LinearMixerState)_state).Parameter = Mathf.Lerp(((LinearMixerState)_state).Parameter, elapsedTime,
                55 * Time.deltaTime);*/

            /*if (_blackBoard.playerMovement.GetVelocity() != Vector3.zero)
            {
                _state = _blackBoard.animancer.Play(idleZip);
            }*/
            
            if (_blackBoard.moveDirection != Vector3.zero)
            {
                _fsm.ChangeState(_sprintState);
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