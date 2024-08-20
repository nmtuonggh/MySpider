namespace SFRemastered
{
    public class Standup: EnemyBaseState
    { 
        public override void EnterState()
        {
            base.EnterState();
            _blackBoard.cantTarget = true;
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
                _blackBoard.animancer.Animator.applyRootMotion = false;
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
