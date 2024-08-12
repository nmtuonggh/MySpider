﻿namespace SFRemastered
{
    public class Die : EnemyBaseState
    {
        public override void EnterState()
        {
            base.EnterState();
            _state.Events.OnEnd += () =>
            {
                Destroy(_enemyFsm.gameObject);
            };
        }
        
        public override StateStatus UpdateState()
        {
            base.UpdateState();
            
            return StateStatus.Running;
        }
        
        public override void ExitState()
        {
            base.ExitState();
        }
    }
}