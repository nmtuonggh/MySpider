using DG.Tweening;
using SFRemastered.Combat;
using UnityEngine;
using UnityEngine.Serialization;

namespace SFRemastered._Game._Scripts.State.Combat.LeapAttack
{
    [CreateAssetMenu(menuName = "ScriptableObjects/States/CombatStates/LeapAttack")]

    public class LeapAttack : CombatBase
    {
        [SerializeField] private CombatController combatController;
        [SerializeField] private IdleCombat.NormalIdleCombat normalIdleCombat;
        
        public override void EnterState()
        {
            base.EnterState();
            _blackBoard.playerMovement.useRootMotion = true;
            
            _blackBoard.playerMovement.transform.DOLookAt(_blackBoard._targetEnemy.transform.position, 0.3f, AxisConstraint.Y);
            var targetPos = (_blackBoard.transform.position - _blackBoard._targetEnemy.transform.position).normalized;
            _blackBoard.playerMovement.transform.DOMove((_blackBoard._targetEnemy.transform.position + targetPos*2), 0.5f);
        }
        
        public override StateStatus UpdateState()
        {
            StateStatus baseStatus = base.UpdateState();
            if(baseStatus != StateStatus.Running)
            {
                return baseStatus;
            }

            if (_state.NormalizedTime > 0.9f)
            {
                _fsm.ChangeState(normalIdleCombat);
                return StateStatus.Success;
            }
            return StateStatus.Running;
        }

        public override void ExitState()
        {
            base.ExitState();
            _blackBoard.playerMovement.useRootMotion = false;
        }
    }
}