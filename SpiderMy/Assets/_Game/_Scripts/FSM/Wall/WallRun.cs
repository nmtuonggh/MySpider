using Animancer;
using DG.Tweening;
using EasyCharacterMovement;
using UnityEngine;

namespace SFRemastered.Wall
{
    [CreateAssetMenu(menuName = "ScriptableObjects/States/WallRun")]
    public class WallRun : StateBase
    {
        [SerializeField] protected float wallRunSpeed;
        [SerializeField] protected JumpOffWall _jumpOffWallState;
        [SerializeField] private ExitWall _exitWallState;
        [SerializeField] private ClipTransition[] _wallRunClip;
        [SerializeField] private LinearMixerTransition _wallRunBlentree;

        private Vector3 startVelocity;
        
        public override void EnterState()
        {
            base.EnterState();
            SetupEnter();
            RotatePlayer();
            Debug.Log("enter wall run");
        }

        public override StateStatus UpdateState()
        {
            StateStatus baseStatus = base.UpdateState();
            if (baseStatus != StateStatus.Running)
            {
                return baseStatus;
            }
            
            HandleAnimation();
            _blackBoard.rigidbody.velocity = _blackBoard.wallMoveDirection * wallRunSpeed;
            
            if (_blackBoard.playerMovement.foudWall && _blackBoard.jump )
            {
                _fsm.ChangeState(_jumpOffWallState);
                return StateStatus.Success;
            }
            
            if (!_blackBoard.playerMovement.foudWall)
            {
                _fsm.ChangeState(_exitWallState);
                return StateStatus.Success;
            }

            return StateStatus.Running;
        }

        public override void ExitState()
        {
            base.ExitState();
            SetupExitState();
            Debug.Log("exit wall run");
        }
        private void RotatePlayer()
        {
            Quaternion targetRotation = Quaternion.LookRotation(-_blackBoard.playerMovement.hit.normal);
            _blackBoard.characterVisual.transform.rotation = targetRotation;
        }
        private void SetupEnter()
        {
            startVelocity = _blackBoard.playerMovement.GetVelocity();
            //Debug.Log("enter velocity: " + startVelocity);
            var distance = _blackBoard.playerMovement.GetRadius();
            var targetPosition = _blackBoard.playerMovement.hit.point + _blackBoard.playerMovement.hit.normal.normalized * distance * 1.3f;
            if (_blackBoard.playerMovement.IsGrounded())
            {
                _blackBoard.playerMovement.transform.DOMove( targetPosition + new Vector3(0,3f,0), 0.2f);
            }
            else
            {
                _blackBoard.playerMovement.transform.DOMove( targetPosition, 0.2f);
            }
            _blackBoard.playerMovement.SetMovementMode(MovementMode.None);
            _blackBoard.rigidbody.useGravity = false;
            _blackBoard.rigidbody.isKinematic = false;
            _blackBoard.rigidbody.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
            //_blackBoard.rigidbody.velocity = velo;
        }
        private void SetupExitState()
        {
            //_blackBoard.playerMovement.foudWall = false;
            var velocity = _blackBoard.rigidbody.velocity.normalized;
            _fsm.transform.DORotate(Quaternion.LookRotation(_fsm.transform.forward.projectedOnPlane(Vector3.up), Vector3.up).eulerAngles, 0.2f);
            _blackBoard.playerMovement.SetMovementMode(MovementMode.Walking);
            _blackBoard.rigidbody.useGravity = false;
            _blackBoard.rigidbody.isKinematic = true;
            _blackBoard.rigidbody.constraints = RigidbodyConstraints.None;
            _blackBoard.playerMovement.SetVelocity(velocity * 6f);
            _blackBoard.characterVisual.transform.DORotate(Quaternion.LookRotation(_blackBoard.playerMovement.transform.forward, Vector3.up).eulerAngles, 0.2f);
            //_state.StartFade(0, 0.1f);
            Debug.Log("exit velocity: " + _blackBoard.playerMovement.GetVelocity());
        }
        private void HandleAnimation()
        {
            if (_blackBoard.wallMoveDirection == Vector3.zero)
            {
                _state = _blackBoard.animancer.Play(_mainAnimation);
            }
            else
            {
                _state = _blackBoard.animancer.Play(_wallRunBlentree);
                ((LinearMixerState)_state).Parameter = Mathf.Lerp(((LinearMixerState)_state).Parameter,
                    Vector3.Angle(_blackBoard.playerMovement.transform.right, _blackBoard.wallMoveDirection), 55 * Time.deltaTime);
            }
        }
    }
}