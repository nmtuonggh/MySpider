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
        [SerializeField] private ZipState _zipState;
        [SerializeField] private LinearMixerTransition _zipBlendTree;
        [SerializeField] private float durationValue;

        public override void EnterState()
        {
            base.EnterState();
            
            _blackBoard.playerMovement.SetMovementMode(MovementMode.None);
            _blackBoard.rigidbody.useGravity = false;
            _blackBoard.rigidbody.isKinematic = false;
            _blackBoard.rigidbody.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
            
            var distance = Vector3.Distance(_blackBoard.playerMovement.transform.position, _blackBoard.findZipPoint.zipPoint);
            _blackBoard.rigidbody.DOMove(_blackBoard.findZipPoint.zipPoint, distance / durationValue);
        }

        public override StateStatus UpdateState()
        {
            StateStatus baseStatus = base.UpdateState();
            if (baseStatus != StateStatus.Running)
            {
                return baseStatus;
            }
            
            _state = _blackBoard.animancer.Play(_zipBlendTree);
            ((LinearMixerState)_state).Parameter = Mathf.Lerp(((LinearMixerState)_state).Parameter, elapsedTime,
                55 * Time.deltaTime);

            if (_blackBoard.playerMovement.GetVelocity() != Vector3.zero)
            {
                _state = _blackBoard.animancer.Play(idleZip);
            }
            
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