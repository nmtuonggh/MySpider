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
            if (Vector3.Distance(_blackBoard.characterController.transform.position, _blackBoard.target.position) > 3)
            {
                Vector3 targetDir = _blackBoard.target.position - transform.position;
                targetDir.y = 0;
                
                //rotate to target with smoothness with CharacterController
                _blackBoard.characterController.transform.rotation = Quaternion.Slerp(transform.rotation,
                    Quaternion.LookRotation(targetDir), rotationSpeed * Time.deltaTime);
                _blackBoard.characterController.Move(_blackBoard.characterController.transform.forward * speed * Time.deltaTime);
            }else
            {
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
        }
    }
}