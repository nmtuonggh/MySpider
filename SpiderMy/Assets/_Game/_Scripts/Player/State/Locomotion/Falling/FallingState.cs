using DG.Tweening;
using UnityEngine;

namespace SFRemastered
{
    public class FallingState : AirBoneState
    {
        [SerializeField] protected SwingState _swingState;
        [SerializeField] protected float speedMultiplier;
        
        protected float incomingVelocity;
        public override void EnterState()
        {
            base.EnterState();
            incomingVelocity = _blackBoard.playerMovement.GetSpeed();
        }

        public override void ExitState()
        {
           var velo = _blackBoard.playerMovement.GetVelocity();
           _blackBoard.playerMovement.SetVelocity(velo.normalized * incomingVelocity);
            base.ExitState();
        }

        public override StateStatus UpdateState()
        {
            StateStatus baseStatus = base.UpdateState();
            if (baseStatus != StateStatus.Running)
            {
                return baseStatus;
            }
            _blackBoard.playerMovement.SetMovementDirection(_blackBoard.moveDirection * speedMultiplier);
            
            return StateStatus.Running;
        }

        public override void FixedUpdateState()
        {
            base.FixedUpdateState();
            var targetRotation = Quaternion.LookRotation(_blackBoard.playerMovement.transform.forward, Vector3.up);
            _blackBoard.characterVisual.transform.rotation = Quaternion.Slerp(_blackBoard.characterVisual.transform.rotation, targetRotation, Time.fixedDeltaTime / 0.3f);
        }
        
        protected bool GroundCheck()
        {
            if (Physics.Raycast(_fsm.transform.position, Vector3.down, 1.8f, _blackBoard.groundLayers))
            {
                return true;
            }

            return false;
        }
    }
}