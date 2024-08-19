namespace SFRemastered
{
    namespace SFRemastered
    {
        public class KnockBack : EnemyBaseState
        { 
            public override void EnterState()
            {
                base.EnterState();
                _blackBoard.cantTarget = true;
                var direction =_blackBoard.characterController.transform.position - _blackBoard.target.obj.transform.position;
                _blackBoard.characterController.Move(direction * 1);
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
            
                if (_state.NormalizedTime >= 1f)
                {
                    _blackBoard.cantTarget = false;
                    //_blackBoard.knockBackHit = false;
                    return StateStatus.Success;
                }
            
                return StateStatus.Running;
            }

            public override void ExitState()
            {
                _blackBoard.animancer.Animator.applyRootMotion = false;
                base.ExitState();
            }
        }
    }
}