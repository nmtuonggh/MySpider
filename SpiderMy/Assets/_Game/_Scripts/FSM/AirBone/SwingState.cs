using DG.Tweening;
using EasyCharacterMovement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace SFRemastered
{
    [CreateAssetMenu(menuName = "ScriptableObjects/States/Swing")]
    public class SwingState : StateBase
    {
        [SerializeField] private IdleState _idleState;
        [SerializeField] private FallState _fallState;
        [SerializeField] private JumpFromSwing _jumpFromSwing;
        [SerializeField] protected Vector3 currentSwingPoint;
        [FormerlySerializedAs("multiplier")] [SerializeField] protected float multiplierVelocity;
        [SerializeField] protected float speedWhenSwing;
        [SerializeField] protected float rotationSpeed;
        
        private SpringJoint _springJoint;
        private Bounds _ropeHolderBounds;
        private Vector3 _randomRopePosition;
        private float angle;
        private float startVelocityMagnitude;
        
        
        //Work in progress, enable physic for swinging simulation
        public override void EnterState()
        {
            currentSwingPoint = _blackBoard.swingPoint.position;
            base.EnterState();
            SetupEnterState();
            RandomPos();
            Swinging();
        }
        public override StateStatus UpdateState()
        {
            //angle = Vector3.Angle(Vector3.down,(_blackBoard.playerSwingPos.position - currentSwingPoint).normalized);
            _blackBoard.rigidbody.AddForce(_blackBoard.moveDirection.normalized * speedWhenSwing);
            if(GroundCheck())
            {
                _fsm.ChangeState(_idleState);
                return StateStatus.Success;
            }
            
            if (!_blackBoard.swing && elapsedTime > 0.8f)
            {
                _fsm.ChangeState(_jumpFromSwing);
                return StateStatus.Success;
            }

            if (!_blackBoard.swing)
            {
                _fsm.ChangeState(_fallState);
                return StateStatus.Success;
            }
            DrawLine();
            

            return StateStatus.Running;
        }

        private void RotationPlayer()
        {
            var direction = _randomRopePosition - _blackBoard.startrope.position;
            _blackBoard.playerMovement.RotateTowardsWithSlerp(direction, true);
            _blackBoard.transform.rotation = Quaternion.LookRotation(direction);
            //Vector3 direction = _randomRopePosition - _blackBoard.startrope.position;
            
           
        }

        public override void FixedUpdateState()
        {
            RotationPlayer();
            base.FixedUpdateState();
        }

        public override void ExitState()
        {
            base.ExitState();
            SetupExitState();

            _blackBoard.lr.positionCount = 0;
            Destroy(_springJoint);
            _blackBoard.swing = false;
        }
        
        private void SetupEnterState()
        {
            startVelocityMagnitude = _blackBoard.playerMovement.GetVelocity().magnitude;
            var velo = _blackBoard.playerMovement.GetVelocity();
            //Vector3 velocity = _blackBoard.playerMovement.GetVelocity();
            _blackBoard.playerMovement.SetMovementMode(MovementMode.None);
            _blackBoard.rigidbody.useGravity = true;
            _blackBoard.rigidbody.isKinematic = false;
            _blackBoard.rigidbody.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
            _blackBoard.rigidbody.velocity = (velo * multiplierVelocity);
            Debug.Log("Velocity" + _blackBoard.rigidbody.velocity.magnitude);
        }

        void Swinging()
        {
            _springJoint = _blackBoard.gameObject.AddComponent<SpringJoint>();
            _springJoint.autoConfigureConnectedAnchor = false;
            _springJoint.connectedAnchor = currentSwingPoint;

            float distanceFromPoint = Vector3.Distance(_blackBoard.playerSwingPos.position, currentSwingPoint);

            _springJoint.maxDistance = distanceFromPoint * 0.8f;
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
            _blackBoard.playerMovement.SetVelocity(velocity.normalized * startVelocityMagnitude);
        }

        private void RandomPos()
        {
            // Get the bounds of the ropeHolder
            _ropeHolderBounds = _blackBoard.ropHolder.GetComponent<Renderer>().bounds;

            // Select a random point within these bounds
            _randomRopePosition = new Vector3(
                Random.Range(_ropeHolderBounds.min.x, _ropeHolderBounds.max.x),
                Random.Range(_ropeHolderBounds.min.y, _ropeHolderBounds.max.y),
                Random.Range(_ropeHolderBounds.min.z, _ropeHolderBounds.max.z)
            );
        }
        private void DrawLine()
        {
            // Set the LineRenderer's positions
            _blackBoard.lr.positionCount = 2;
            _blackBoard.lr.SetPosition(0, _blackBoard.startrope.position);
            _blackBoard.lr.SetPosition(1, _randomRopePosition);
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
