using UnityEngine;

namespace SFRemastered.Wall
{    
    [CreateAssetMenu(menuName = "ScriptableObjects/States/IdleWall")]
    public class IdleWall : StateBase
    {
        [SerializeField] private WallRun _wallRunState;
        public override void EnterState()
        {
            RotatePlayer();
            base.EnterState();
        }

        private void RotatePlayer()
        {
            Quaternion targetRotation = Quaternion.LookRotation(-_blackBoard.playerMovement.hit.normal);
            _blackBoard.characterVisual.transform.rotation = targetRotation;
        }

        public override StateStatus UpdateState()
        {
            StateStatus baseStatus = base.UpdateState();
            if (baseStatus != StateStatus.Running)
            {
                return baseStatus;
            }

            _blackBoard.rigidbody.velocity = Vector3.zero;
            
            if (_blackBoard.wallMoveDirection != Vector3.zero)
            {
                _fsm.ChangeState(_wallRunState);
                return StateStatus.Success;
            }
            
            return StateStatus.Running;

        }

        public override void ExitState()
        {
            base.ExitState();
        }
    }
}