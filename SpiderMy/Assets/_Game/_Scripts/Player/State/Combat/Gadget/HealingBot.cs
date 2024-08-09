using System.Collections.Generic;
using UnityEngine;

namespace SFRemastered._Game._Scripts.Player.State.Combat.Gadget
{
    namespace SFRemastered._Game._Scripts.Player.State.Combat.Gadget
    {
        [CreateAssetMenu(menuName = "ScriptableObjects/States/CombatStates/HeallingBot")]
        public class HealingBot : StateBase
        {
            private GameObject healingBot;
            public override void EnterState()
            {
                base.EnterState();
                healingBot = _blackBoard.healingBotSO.Spawn(_blackBoard.playerMovement.transform.position,
                    Quaternion.identity, _blackBoard.poolManager.transform);
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
        }
    }
}