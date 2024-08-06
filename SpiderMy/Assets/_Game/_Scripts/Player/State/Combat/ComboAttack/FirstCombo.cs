using SFRemastered.Combat;
using UnityEngine;

namespace SFRemastered._Game._Scripts.State.Combat.ComboAttack
{
    [CreateAssetMenu(menuName = "ScriptableObjects/States/CombatStates/FirstCombo")]
    public class FirstCombo : ComboAttackBase
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
    }
}