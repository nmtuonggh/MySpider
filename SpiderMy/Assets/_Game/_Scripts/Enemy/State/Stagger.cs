using UnityEngine;

namespace SFRemastered
{
    public class Stagger : EnemyBaseState
    { 
        private Vector3 direction;
        private Vector3 targetPosition;
        public override void EnterState()
        {
            base.EnterState();
            direction =_blackBoard.characterController.transform.position - _blackBoard.target.obj.transform.position;
           
            _blackBoard.lineRenderer.positionCount = 0;
            _blackBoard.animancer.Animator.applyRootMotion = true;
        }
        

        public override StateStatus UpdateState()
        {
            StateStatus baseStatus = base.UpdateState();
            if(baseStatus != StateStatus.Running)
            {
                return baseStatus;
            }
            
            _blackBoard.characterController.Move(direction * Time.deltaTime);
            
            if (_state.NormalizedTime >= .8f)
            {
                _blackBoard.staggerHit = false;
                return StateStatus.Success;
            }
            
            return StateStatus.Running;
        }

        public override void ExitState()
        {
            base.ExitState();
            
            _blackBoard.animancer.Animator.applyRootMotion = false;
        }
    }
}