namespace SFRemastered
{
    public class Stagger : EnemyBaseState
    {
        private bool done;
        public override void EnterState()
        {
            done = false;
            base.EnterState();
            _state.Events.OnEnd = () =>
            {
               done = true;
            };
        }
        

        public override StateStatus UpdateState()
        {
            StateStatus baseStatus = base.UpdateState();
            if(baseStatus != StateStatus.Running)
            {
                return baseStatus;
            }
            
            if (_state.NormalizedTime >= .8f)
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