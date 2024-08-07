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
        }

        public override StateStatus UpdateState()
        {
            StateStatus baseStatus = base.UpdateState();
            if (baseStatus != StateStatus.Running)
            {
                return baseStatus;
            }
            //Lerp Rotation to target
            /*var targetPos = new Vector3(_blackBoard.target.obj.transform.position.x, 0,
                _blackBoard.target.obj.transform.position.z);*/
            if (Vector3.Distance(_blackBoard.characterController.transform.position, _blackBoard.target.obj.transform.position) > 0)
            {
                //Move to target
                _blackBoard.characterController.transform.LookAt(_blackBoard.target.obj.transform.position);
                _blackBoard.characterController.Move(_blackBoard.characterController.transform.forward * speed * Time.deltaTime);
            }else
            {
                return StateStatus.Success;
            }
            
            transform.DOMove(transform.position + (transform.forward / 1), .5f);

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