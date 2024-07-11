using DG.Tweening;
using EasyCharacterMovement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SFRemastered
{
    public class SwingState : StateBase
    {
        [SerializeField] private IdleState _idleState;
        [SerializeField] private FallState _fallState;
        protected Transform swingPoint;
        [SerializeField] protected SpringJoint _springJoint;
        
        
        //Work in progress, enable physic for swinging simulation
        public override void EnterState()
        {
            base.EnterState();
            Vector3 velocity = _blackBoard.playerMovement.GetVelocity();
            _blackBoard.playerMovement.SetMovementMode(MovementMode.None);
            _blackBoard.rigidbody.useGravity = true;
            _blackBoard.rigidbody.isKinematic = false;
            _blackBoard.rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            _blackBoard.rigidbody.velocity = velocity * 2f;
            
        }

        public override StateStatus UpdateState()
        {
            _blackBoard.playerMovement.SetMovementDirection(_blackBoard.moveDirection);
            
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

        public override void FixedUpdateState()
        {
            base.FixedUpdateState();
        }

        public override void ExitState()
        {
            base.ExitState();

            Vector3 velocity = _blackBoard.rigidbody.velocity.projectedOnPlane(Vector3.up);
            _fsm.transform.DORotate(Quaternion.LookRotation(_fsm.transform.forward.projectedOnPlane(Vector3.up), Vector3.up).eulerAngles, 0.2f);
            _blackBoard.playerMovement.SetMovementMode(MovementMode.Walking);
            _blackBoard.rigidbody.useGravity = false;
            _blackBoard.rigidbody.isKinematic = true;
            _blackBoard.rigidbody.constraints = RigidbodyConstraints.None;
            _blackBoard.playerMovement.SetVelocity(velocity);
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
