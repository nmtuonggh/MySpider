using UnityEngine;

namespace SFRemastered._Game._Scripts.State.Combat.IdleCombat
{
    namespace SFRemastered.Combat
    {
        [CreateAssetMenu(menuName = "ScriptableObjects/States/CombatStates/LowIdleCombat")]
        public class LowIdleCombat : IdlesCombatBase
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

        }
    }
}