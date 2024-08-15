using DG.Tweening;
using NodeCanvas.Framework;
using UnityEngine;

namespace SFRemastered
{
    public class ChaseState : EnemyBaseState
    {
        [SerializeField] private float rotationSpeed;
        [SerializeField] private float speed;

        public override void EnterState()
        {
            base.EnterState();
            _blackBoard.lineRenderer.positionCount = 0;
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
            
            if (Vector3.Distance(_blackBoard.characterController.transform.position, groundTargetPosition) > 0)
            {
                //Move to target
                _blackBoard.characterController.transform.LookAt(groundTargetPosition);
                _blackBoard.characterController.Move(_blackBoard.characterController.transform.forward * speed * Time.deltaTime);
            }else
            {
                return StateStatus.Success;
            }
            
            //transform.DOMove(transform.position + (transform.forward / 1), .5f);

            return StateStatus.Running;
        }

        public override void FixedUpdateState()
        {
            base.FixedUpdateState();
            
        }

        public override void ExitState()
        {
            base.ExitState();
        }
    }
}