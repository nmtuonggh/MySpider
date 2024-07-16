using UnityEngine;

namespace SFRemastered.Wall
{
    [CreateAssetMenu(menuName = "ScriptableObjects/States/WallRun")]
    public class WallRun : WallState
    {
        [SerializeField] private IdleWall _idleWallState;
        [SerializeField] private ExitWall _exitWallState;
        public override void EnterState()
        {
            base.EnterState();
        }

        public override StateStatus UpdateState()
        {
            RotateVisualPlayer();
            StateStatus baseStatus = base.UpdateState();
            if (baseStatus != StateStatus.Running)
            {
                return baseStatus;
            }
            
            _blackBoard.rigidbody.velocity = _blackBoard.wallMoveDirection * wallRunSpeed;

            if (_blackBoard.wallMoveDirection == Vector3.zero)
            {
                _fsm.ChangeState(_idleWallState);
                return StateStatus.Success;
            }
            
            return StateStatus.Running;
        }

        private void RotateVisualPlayer()
        {
            Vector3 perpendicularDirection = Vector3.Cross(_blackBoard.playerMovement.hit.normal, Vector3.right);
            Quaternion targetRotation = Quaternion.LookRotation(perpendicularDirection, _blackBoard.playerMovement.hit.normal);
            _blackBoard.characterVisual.transform.rotation = targetRotation;
        }

        public override void ExitState()
        {
            base.ExitState();
        }
    }
}