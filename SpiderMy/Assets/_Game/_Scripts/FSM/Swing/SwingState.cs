using DG.Tweening;
using EasyCharacterMovement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SFRemastered
{
    [CreateAssetMenu(menuName = "ScriptableObjects/States/Swing")]
    public class SwingState : StateBase
    {
        [SerializeField] private IdleState _idleState;
        [SerializeField] private FallState _fallState;
        [SerializeField] protected Vector3 currentSwingPoint;
        [SerializeField] protected float multiplier;
        protected SpringJoint springJoint;
        
        
        //Work in progress, enable physic for swinging simulation
        public override void EnterState()
        {
            Debug.Log("Enter");
            currentSwingPoint = _blackBoard.swingPoint.position;
            base.EnterState();
            Vector3 velocity = _blackBoard.playerMovement.GetVelocity();
            _blackBoard.playerMovement.SetMovementMode(MovementMode.None);
            _blackBoard.rigidbody.useGravity = true;
            _blackBoard.rigidbody.isKinematic = false;
            _blackBoard.rigidbody.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
            _blackBoard.rigidbody.velocity = (velocity * multiplier);
            Debug.Log("Velocity" + _blackBoard.rigidbody.velocity.magnitude);

            StartGrapple();
        }

        public override StateStatus UpdateState()
        {
            if(GroundCheck())
            {
                _fsm.ChangeState(_idleState);
                return StateStatus.Success;
            }
            
            if (!_blackBoard.swing )
            {
                _fsm.ChangeState(_fallState);
                return StateStatus.Success;
            }

            
            
            return StateStatus.Running;
        }

        private void RotationPlayer()
        {
            _blackBoard.playerMovement.RotateTowardsWithSlerp(_blackBoard.rigidbody.velocity.normalized, true);
        }

        public override void FixedUpdateState()
        {
            RotationPlayer();
            DrawLine();
            base.FixedUpdateState();
        }

        public override void ExitState()
        {
            base.ExitState();
            Vector3 dampingForce = -_blackBoard.rigidbody.velocity * 1;
            _blackBoard.rigidbody.AddForce(dampingForce, ForceMode.VelocityChange);
            Vector3 velocity = _blackBoard.rigidbody.velocity / multiplier;
            
            _fsm.transform.DORotate(Quaternion.LookRotation(_fsm.transform.forward.projectedOnPlane(Vector3.up), Vector3.up).eulerAngles, 0.2f);
            _blackBoard.playerMovement.SetMovementMode(MovementMode.Walking);
            _blackBoard.rigidbody.useGravity = false;
            _blackBoard.rigidbody.isKinematic = true;
            _blackBoard.rigidbody.constraints = RigidbodyConstraints.None;
            _blackBoard.playerMovement.SetVelocity(velocity);

            _blackBoard.lr.positionCount = 0;
            Destroy(springJoint);
            _blackBoard.swing = false;
        } 
        void StartGrapple()
        {
            springJoint = _blackBoard.gameObject.AddComponent<SpringJoint>();
            springJoint.autoConfigureConnectedAnchor = false;
            springJoint.connectedAnchor = currentSwingPoint;

            float distanceFromPoint = Vector3.Distance(_blackBoard.playerHand.position, currentSwingPoint);

            springJoint.maxDistance = distanceFromPoint * 0.8f;
            springJoint.minDistance = distanceFromPoint * 0.25f;

            springJoint.spring = 4.5f;
            springJoint.damper = 7f;
            springJoint.massScale = 1f;
            _blackBoard.lr.positionCount = 2;
        }
        
        private void DrawLine()
        {
            _blackBoard.lr.positionCount = 2;
            _blackBoard.lr.SetPosition(0, _blackBoard.playerHand.position);
            _blackBoard.lr.SetPosition(1, currentSwingPoint);
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
