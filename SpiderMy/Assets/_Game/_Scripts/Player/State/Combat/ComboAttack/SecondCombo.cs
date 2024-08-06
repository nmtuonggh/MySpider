using SFRemastered._Game._Scripts.State.Combat.ComboAttack;
using UnityEngine;

namespace SFRemastered.Combat
{    [CreateAssetMenu(menuName = "ScriptableObjects/States/CombatStates/SecondCombo")]

    public class SecondCombo : ComboAttackBase
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