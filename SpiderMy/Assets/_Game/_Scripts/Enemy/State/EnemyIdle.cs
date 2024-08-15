using NodeCanvas.Framework;
using ParadoxNotion;
using SFRemastered._Game._Scripts.ReferentSO;
using UnityEngine;

namespace SFRemastered
{
    public class EnemyIdle : EnemyBaseState
    {
        public float rotationSpeed = 3f;
        
        //public GameObjectRef player;
        public override void EnterState()
        {
            base.EnterState();
            _blackBoard.lineRenderer.positionCount = 0;
        }
        
        public override StateStatus UpdateState()
        {
            StateStatus baseStatus = base.UpdateState();
            if(baseStatus != StateStatus.Running)
            {
                return baseStatus;
            }

            var target = _blackBoard.target.obj.transform.position;
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