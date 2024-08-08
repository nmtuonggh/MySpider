using UnityEngine;

namespace SFRemastered
{
    public class Retreat : EnemyBaseState
    {
        [SerializeField] private float randomRange;
        [SerializeField] private float minRange = 2.5f;
        [SerializeField] private float maxRange = 5f;
        [SerializeField] private float rotationSpeed = 3f;
        [SerializeField] private float speed = 3f;
        public override void EnterState()
        {
            randomRange = Random.Range(minRange, maxRange);
            base.EnterState();
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
            
            _blackBoard.characterController.Move(-_blackBoard.characterController.transform.forward * speed * Time.deltaTime);
            
            var targetPosition = _blackBoard.target.obj.transform.position;
            var groundTargetPosition = new Vector3(targetPosition.x, _blackBoard.characterController.transform.position.y, targetPosition.z);

            if (Vector3.Distance(
                    _blackBoard.characterController.transform.position, groundTargetPosition) > randomRange)
            {
                return StateStatus.Failure;
            }
            
            return StateStatus.Running;
        }
        
        public override void ExitState()
        {
            base.ExitState();
        }
    }
}