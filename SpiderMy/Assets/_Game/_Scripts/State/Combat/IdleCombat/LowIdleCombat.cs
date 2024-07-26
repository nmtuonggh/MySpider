﻿using UnityEngine;

namespace SFRemastered.Combat
{
    using UnityEngine;

    namespace SFRemastered.Combat
    {
        [CreateAssetMenu(menuName = "ScriptableObjects/States/CombatStates/LowIdleCombat")]
        public class LowIdleCombat : CombatState
        {
            public override void EnterState()
            {
                base.EnterState();
                _blackBoard.playerMovement.SetMovementDirection(Vector3.zero);
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