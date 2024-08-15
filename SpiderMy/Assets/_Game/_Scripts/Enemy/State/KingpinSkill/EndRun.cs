namespace SFRemastered.KingpinSkill
{
    public class EndRun : EnemyBaseState
    {
        public override void EnterState()
        {
            base.EnterState();
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