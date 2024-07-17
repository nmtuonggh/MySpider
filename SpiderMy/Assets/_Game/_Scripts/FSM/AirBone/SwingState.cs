using DG.Tweening;
using EasyCharacterMovement;
using System.Collections;
using System.Collections.Generic;
using Animancer;
using UnityEngine;
using UnityEngine.Serialization;

namespace SFRemastered
{
    [CreateAssetMenu(menuName = "ScriptableObjects/States/Swing")]
    public class SwingState : AirBoneState
    {
        [SerializeField] private IdleState _idleState;
        [SerializeField] private FallState _fallState;
        [SerializeField] private JumpFromSwing _jumpFromSwing;
        [SerializeField] protected Vector3 currentSwingPoint;
        [SerializeField] protected float startSwingVelocity;
        [SerializeField] protected float speedWhenSwing;
        //[SerializeField] private LinearMixerTransition _swingAnimBlendTree;
        [SerializeField] private ClipTransition[] _ListAnim;
        [SerializeField] private int _swingAnimCount;
        //[SerializeField] private ClipTransition _swingLoopAnimation;
        
        private SpringJoint _springJoint;
        private Bounds _ropeHolderBounds;
        private Vector3 _randomRopePosition;
        private float angle;
        private int animIndex;
        
        
        //Work in progress, enable physic for swinging simulation
        public override void EnterState()
        {
            currentSwingPoint = _blackBoard.swingPoint.position;
            animIndex = Random.Range(0, _swingAnimCount);
            base.EnterState();
            SetupEnterState();
            RandomRopeShotPosition();
            Swinging();
        }
        public override StateStatus UpdateState()
        {
            base.UpdateState();
            
            _blackBoard.rigidbody.AddForce(_blackBoard.moveDirection.normalized * speedWhenSwing);
            if(GroundCheck())
            {
                _fsm.ChangeState(_idleState);
                return StateStatus.Success;
            }
            
            if (!_blackBoard.swing)
            {
                if(elapsedTime is >= .3f and < 0.8f)
                {
                    _fsm.ChangeState(_fallState);
                    return StateStatus.Success;
                }
                if (elapsedTime >= 0.8f)
                {
                    _fsm.ChangeState(_jumpFromSwing);
                    return StateStatus.Success;
                }
            }
            
            DrawLine();
            SwitchAnim();
            
            return StateStatus.Running;
        }
        
        public override void FixedUpdateState()
        {
            base.FixedUpdateState();
            RotationPlayerWhileSwing();
        }

        public override void ExitState()
        {
            base.ExitState();
            SetupExitState();

            _blackBoard.lr.positionCount = 0;
            Destroy(_springJoint);
            _blackBoard.swing = false;
        }

        private void SwitchAnim()
        {
            //Debug.Log(_blackBoard.rigidbody.velocity.magnitude);
            if(_blackBoard.rigidbody.velocity.magnitude < 5f)
            {
                _state = _blackBoard.animancer.Play(_mainAnimation);
            }
            else
            {
                /*_state = _blackBoard.animancer.Play(_swingAnimBlendTree);
                ((LinearMixerState)_state).Parameter = animIndex;*/
                _state = _blackBoard.animancer.Play(_ListAnim[animIndex]);
            }
        }
        private void RotationPlayerWhileSwing()
        {
            var direction = _randomRopePosition - _blackBoard.startrope.position;
            _blackBoard.playerMovement.RotateTowardsWithSlerp(_blackBoard.rigidbody.velocity.normalized,  false);
            Quaternion rotation = Quaternion.LookRotation( _blackBoard.playerMovement.transform.forward, direction.normalized);
            _blackBoard.playerMovement.transform.rotation = rotation;
        }
        private void SetupEnterState()
        {
            var velocity = _blackBoard.playerMovement.GetVelocity().normalized;
            _blackBoard.playerMovement.SetMovementMode(MovementMode.None);
            _blackBoard.rigidbody.useGravity = true;
            _blackBoard.rigidbody.isKinematic = false;
            _blackBoard.rigidbody.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
            _blackBoard.rigidbody.velocity = (velocity * startSwingVelocity);
            //Debug.Log("Velocity" + _blackBoard.rigidbody.velocity.magnitude);
        }
        private void Swinging()
        {
            _springJoint = _blackBoard.gameObject.AddComponent<SpringJoint>();
            _springJoint.autoConfigureConnectedAnchor = false;
            _springJoint.connectedAnchor = currentSwingPoint;

            var distanceFromPoint = Vector3.Distance(_blackBoard.playerSwingPos.position, currentSwingPoint);

            _springJoint.maxDistance = distanceFromPoint * 0.5f;
            _springJoint.minDistance = distanceFromPoint * 0.25f;

            _springJoint.spring = 4.5f;
            _springJoint.damper = 7f;
            _springJoint.massScale = 1f;
            _blackBoard.lr.positionCount = 2;
        }
        private void SetupExitState()
        {
            Vector3 velocity = _blackBoard.rigidbody.velocity;
            
            _fsm.transform.DORotate(Quaternion.LookRotation(_fsm.transform.forward.projectedOnPlane(Vector3.up), Vector3.up).eulerAngles, 0.2f);
            _blackBoard.playerMovement.SetMovementMode(MovementMode.Walking);
            _blackBoard.rigidbody.useGravity = false;
            _blackBoard.rigidbody.isKinematic = true;
            _blackBoard.rigidbody.constraints = RigidbodyConstraints.None;
            _blackBoard.playerMovement.SetVelocity(velocity.normalized * startSwingVelocity);
        }
        private void RandomRopeShotPosition()
        {
            _ropeHolderBounds = _blackBoard.ropHolder.GetComponent<Renderer>().bounds;
            _randomRopePosition = new Vector3(
                Random.Range(_ropeHolderBounds.min.x, _ropeHolderBounds.max.x),
                Random.Range(_ropeHolderBounds.min.y, _ropeHolderBounds.max.y),
                Random.Range(_ropeHolderBounds.min.z, _ropeHolderBounds.max.z)
            );
        }
        private void DrawLine()
        {
            // Set the LineRenderer's positions
            _blackBoard.lr.positionCount = 3;
            _blackBoard.lr.SetPosition(2, _randomRopePosition);
            _blackBoard.lr.SetPosition(1, _blackBoard.startrope.position);
            _blackBoard.lr.SetPosition(0, _blackBoard.startrope.position + Vector3.down * .5f);
        } 
        private bool GroundCheck()
        {
            if(Physics.Raycast(_fsm.transform.position, Vector3.down, 0.3f, _blackBoard.groundLayers))
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
