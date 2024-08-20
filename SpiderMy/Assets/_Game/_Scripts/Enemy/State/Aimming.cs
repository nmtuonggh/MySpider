using UnityEngine;

namespace SFRemastered
{
    public class Aimming : EnemyBaseState
    {
        public float rotationSpeed = 3f;
        public float aimTime;
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
            
            _blackBoard.characterController.Move(Vector3.zero);
            
            var lastPos = _blackBoard.target.obj.transform.position;
            var shootPos = _blackBoard.shootPosition.transform.position;
            var height = _blackBoard.target.obj.GetComponent<CapsuleCollider>().height * 2/3;
            var target = lastPos + Vector3.up * height;
            
            var direction = (target - shootPos).normalized;
            var extendedTarget = target + direction * 0.1f;
            
            _blackBoard.lineRenderer.positionCount = 2;
            _blackBoard.lineRenderer.SetPosition(0, shootPos);
            _blackBoard.lineRenderer.SetPosition(1, extendedTarget);
            
            Vector3 targetDir = lastPos -  transform.position;
            targetDir.y = 0;
            
            //rotate to target with smoothness with CharacterController
            _blackBoard.characterController.transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.LookRotation(targetDir), rotationSpeed * Time.deltaTime);

            if (elapsedTime >= aimTime)
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