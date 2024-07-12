using UnityEngine;

namespace SFRemastered
{
    [CreateAssetMenu(menuName = "ScriptableObjects/States/Dive")]
    public class DiveState : FallDownBaseState
    {
        public override void EnterState()
        {
            base.EnterState();
        }

        public override void ExitState()
        {
            base.ExitState();
        }


        public override StateStatus UpdateState()
        {
            StateStatus baseStatus = base.UpdateState();
            if (baseStatus != StateStatus.Running)
            {
                return baseStatus;
            }
            
            return StateStatus.Running;
        }
    }
}