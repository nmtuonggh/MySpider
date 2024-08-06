using ParadoxNotion;
using UnityEngine;

namespace SFRemastered
{
    public class EnemyIdle : EnemyBaseState
    {
        public float rotationSpeed = 3f;
        public override void EnterState()
        {
            base.EnterState();
        }
        
        public override StateStatus UpdateState()
        {
            StateStatus baseStatus = base.UpdateState();
            if(baseStatus != StateStatus.Running)
            {
                return baseStatus;
            }
            
            Vector3 targetDir = _blackBoard.target.position - transform.position;
            targetDir.y = 0;
                
            //rotate to target with smoothness with CharacterController
            _blackBoard.characterController.transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(targetDir), rotationSpeed * Time.deltaTime);
            
            return StateStatus.Running;
        }

        public override void ExitState()
        {
            base.ExitState();
        }
    }
}