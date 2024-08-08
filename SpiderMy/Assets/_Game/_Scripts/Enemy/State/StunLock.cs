using UnityEngine;

namespace SFRemastered
{
    public class StunLock : EnemyBaseState
    {
        private float currentTime;
        
        public override void EnterState()
        {
            base.EnterState();
            currentTime = 0;
        }
        

        public override StateStatus UpdateState()
        {
            StateStatus baseStatus = base.UpdateState();
            if(baseStatus != StateStatus.Running)
            {
                return baseStatus;
            }
            
            currentTime += Time.deltaTime;

            if (currentTime >= _blackBoard.stunLockTime)
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