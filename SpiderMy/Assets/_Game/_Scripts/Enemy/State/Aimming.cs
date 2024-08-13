using UnityEngine;

namespace SFRemastered
{
    public class Aimming : EnemyBaseState
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
            
            _blackBoard.characterController.Move(Vector3.zero);
            var target = _blackBoard.target.obj.transform.position;
            _blackBoard.lineRenderer.positionCount = 2;
            _blackBoard.lineRenderer.SetPosition(0, _blackBoard.shootPosition.transform.position);
            _blackBoard.lineRenderer.SetPosition(1, target);
            
            Vector3 targetDir = target -  transform.position;
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