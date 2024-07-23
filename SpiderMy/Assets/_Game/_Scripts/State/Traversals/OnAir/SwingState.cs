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
        [SerializeField] private LandRollState _landRollState;
        [SerializeField] private JumpFromSwingLow _jumpFromSwingLow;
        [SerializeField] private JumpFromSwing _jumpFromSwing;
        [SerializeField] protected Vector3 currentSwingPoint;
        [SerializeField] protected float startSwingVelocity;
        [SerializeField] protected float speedWhenSwing;
        [SerializeField] private ClipTransition[] _ListAnim;

        [FormerlySerializedAs("_swingAnimCount")] [SerializeField]
        private int swingAnimCount;

        private SpringJoint _springJoint;
        private Bounds _ropeHolderBounds;
        private Vector3 _randomRopePosition;
        private float angle;
        private int animIndex;

        
        public override void EnterState()
        {
            currentSwingPoint = _blackBoard.swingPoint.position;
            animIndex = Random.Range(0, swingAnimCount);
            base.EnterState();
            SetupEnterState();
            RandomRopeShotPosition();
            Swinging();
        }

        public override StateStatus UpdateState()
        {
            StateStatus baseStatus = base.UpdateState();
            if (baseStatus != StateStatus.Running)
            {
                return baseStatus;
            }

            SwitchAnim();

            _blackBoard.rigidbody.AddForce(_blackBoard.moveDirection.normalized * speedWhenSwing);
            if (GroundCheck())
            {
                _fsm.ChangeState(_landRollState);
                return StateStatus.Success;
            }

            if (!_blackBoard.swing)
            {
                if (_blackBoard.rigidbody.velocity.y <= 0)
                {
                    _fsm.ChangeState(_jumpFromSwingLow);
                    return StateStatus.Success;
                }

                if (_blackBoard.rigidbody.velocity.y > 0)
                {
                    _fsm.ChangeState(_jumpFromSwing);
                    return StateStatus.Success;
                }
            }

            DrawLine();

            return StateStatus.Running;
        }

        public override void FixedUpdateState()
        {
            base.FixedUpdateState();
            //RotationPlayerWhileSwing();
            RotateVisual();
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
            if (_blackBoard.rigidbody.velocity.magnitude < 5f)
            {
                _state = _blackBoard.animancer.Play(_mainAnimation);
            }
            else
            {
                _state = _blackBoard.animancer.Play(_ListAnim[animIndex]);
            }
        }

        private void RotationPlayerWhileSwing()
        {
            var direction = _randomRopePosition - _blackBoard.startrope.position;
            _blackBoard.playerMovement.RotateTowardsWithSlerp(_blackBoard.rigidbody.velocity.normalized, false);
            Quaternion rotation =
                Quaternion.LookRotation(_blackBoard.playerMovement.transform.forward, direction.normalized);
            _blackBoard.playerMovement.transform.rotation = rotation;
        }

        private void RotateVisual()
        {
            /*Quaternion targetRot =  Quaternion.Euler(Vector3.zero);
            Vector3 threadDirection = (_randomRopePosition - _blackBoard.startrope.position).normalized;
            Vector3 planeNormalX = Vector3.Cross(_blackBoard.transform.right, threadDirection);
            Quaternion rotationX = Quaternion.LookRotation(planeNormalX);
            Vector3 planeNormalZ = Vector3.Cross(_blackBoard.transform.forward, threadDirection);
            Quaternion rotationZ = Quaternion.LookRotation(planeNormalZ);

            Vector3 eulers = new Vector3(rotationX.eulerAngles.x, 0, rotationZ.eulerAngles.x);
            targetRot = Quaternion.Euler(eulers);

            _blackBoard.characterVisual.transform.localRotation = Quaternion.Slerp(_blackBoard.characterVisual.transform.localRotation, targetRot, 0.2f);*/
            
            Vector3 ropeDirection = (_randomRopePosition - _blackBoard.startrope.position).normalized;
            Vector3 velocityDirection = _blackBoard.rigidbody.velocity.normalized;
            
            if (velocityDirection != Vector3.zero && ropeDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(velocityDirection, ropeDirection);
                
                _blackBoard.characterVisual.transform.rotation = Quaternion.Slerp(_blackBoard.characterVisual.transform.rotation,
                    targetRotation,
                    Time.fixedDeltaTime * 5f
                );
            }
        }

        private void SetupEnterState()
        {
            var velocity = _blackBoard.playerMovement.GetVelocity().normalized;
            _blackBoard.playerMovement.SetMovementMode(MovementMode.None);
            _blackBoard.rigidbody.useGravity = true;
            _blackBoard.rigidbody.isKinematic = false;
            _blackBoard.rigidbody.constraints =
                RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
            //_blackBoard.rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
            _blackBoard.rigidbody.velocity = (velocity * startSwingVelocity);
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

            _fsm.transform.DORotate(
                Quaternion.LookRotation(_fsm.transform.forward.projectedOnPlane(Vector3.up), Vector3.up).eulerAngles,
                0.2f);
            _blackBoard.playerMovement.SetMovementMode(MovementMode.Walking);
            _blackBoard.rigidbody.useGravity = false;
            _blackBoard.rigidbody.isKinematic = true;
            _blackBoard.rigidbody.constraints = RigidbodyConstraints.None;
            //_blackBoard.rigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
            //_blackBoard.characterVisual.transform.DORotate(Quaternion.LookRotation(_blackBoard.playerMovement.transform.forward, Vector3.up).eulerAngles, 0.3f);
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