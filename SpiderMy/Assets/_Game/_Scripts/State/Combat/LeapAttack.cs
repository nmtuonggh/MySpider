using UnityEngine;

namespace SFRemastered.Combat
{
    [CreateAssetMenu(menuName = "ScriptableObjects/States/CombatStates/LeapAttack")]

    public class LeapAttack : CombatState
    {
        public override void EnterState()
        {
            base.EnterState();
        }
        
        public override StateStatus UpdateState()
        {
            StateStatus baseStatus = base.UpdateState();
            if(baseStatus != StateStatus.Running)
            {
                return baseStatus;
            }
            return StateStatus.Running;
        }

        public override void ExitState()
        {
            base.ExitState();
        }
    }
}