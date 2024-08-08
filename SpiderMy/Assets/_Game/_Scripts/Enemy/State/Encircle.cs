using Animancer;
using UnityEngine;

namespace SFRemastered
{
    public class Encircle : EnemyBaseState
    {
        [SerializeField] float speed = 3f;
        [SerializeField] ClipTransition _moveLeft;
        [SerializeField] ClipTransition _moveRight;
        private int randomDirection;

        public override void EnterState()
        {
            base.EnterState();
            randomDirection = Random.Range(0, 2);
        }

        public override StateStatus UpdateState()
        {
            StateStatus baseStatus = base.UpdateState();
            if (baseStatus != StateStatus.Running)
            {
                return baseStatus;
            }
            
            var targetPosition = _blackBoard.target.obj.transform.position;
            var groundTargetPosition = new Vector3(targetPosition.x, _blackBoard.characterController.transform.position.y, targetPosition.z);
            
            if (elapsedTime < 5f)
            {
                var moveDirection = randomDirection == 1 ? _blackBoard.characterController.transform.right : -_blackBoard.characterController.transform.right;
                _blackBoard.characterController.Move(moveDirection * speed * Time.deltaTime);
                _state = _blackBoard.animancer.Play(randomDirection == 0 ? _moveLeft : _moveRight);

                _blackBoard.characterController.transform.LookAt(groundTargetPosition);
            }
            else
            {
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