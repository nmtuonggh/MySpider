using DG.Tweening;
using UnityEngine;

namespace SFRemastered.Combat
{
    [CreateAssetMenu(menuName = "ScriptableObjects/States/CombatStates/LeapAttack")]

    public class LeapAttack : StateBase
    {
        [SerializeField] private AttackController _attackController;
        [SerializeField] private IdleCombat _idleCombat;
        
        public override void EnterState()
        {
            base.EnterState();
            _blackBoard.playerMovement.useRootMotion = true;
            _blackBoard.playerMovement.rootmotionSpeedMult = 1;
            
            _blackBoard.playerMovement.transform.DOLookAt(_blackBoard._targetEnemy.transform.position, 0.3f, AxisConstraint.Y);
            var targetPos = (_blackBoard._targetEnemy.transform.position - _blackBoard.transform.position).normalized;
            _blackBoard.playerMovement.transform.DOMove(_blackBoard._targetEnemy.transform.position - targetPos*2, 0.5f);
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
                _fsm.ChangeState(_idleCombat);
                return StateStatus.Success;
            }
            return StateStatus.Running;
        }

        public override void ExitState()
        {
            base.ExitState();
            _blackBoard.playerMovement.useRootMotion = false;
            //_blackBoard.playerMovement.rootmotionSpeedMult = 1;
        }
    }
}