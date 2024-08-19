using SFRemastered._Game._Scripts.State.Combat;
using UnityEngine;

namespace SFRemastered._Game._Scripts.Player.State.Combat.Gadget
{
    public class GadgetBase : CombatBase
    {
        public int maxStack;
        public float coolDown;
        public float currentStack;
        public float currentCoolDown;
        public override void EnterState()
        {
            base.EnterState();
        }
        
        public override StateStatus UpdateState()
        {
            return StateStatus.Running;
        }
        
        public override void ExitState()
        {
            base.ExitState();
        }

        public void ReplenishStack()
        {
            if (currentCoolDown <= 0 && currentStack < maxStack)
            {
                currentStack++;
                currentCoolDown = coolDown;
            }
        }
    }
}