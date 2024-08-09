using System.Collections.Generic;
using UnityEngine;

namespace SFRemastered._Game._Scripts.Player.State.Combat.Gadget
{
    namespace SFRemastered._Game._Scripts.Player.State.Combat.Gadget
    {
        [CreateAssetMenu(menuName = "ScriptableObjects/States/CombatStates/HeallingBot")]
        public class HealingBot : StateBase
        {
            public override void EnterState()
            {
                base.EnterState();
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

            public override void ExitState()
            {
                base.ExitState();
            }

            public void SetGadgetIndex(int x)
            {
            }
        }
    }
}