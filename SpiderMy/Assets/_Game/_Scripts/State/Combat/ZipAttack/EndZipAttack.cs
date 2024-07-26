using DG.Tweening;
using UnityEngine;

namespace SFRemastered.Combat.ZipAttack
{
    [CreateAssetMenu(menuName = "ScriptableObjects/States/CombatStates/EndZipAttack")]
    public class EndZipAttack : StateBase
    {
        [SerializeField] private IdleCombat _idleCombat;

        public override void EnterState()
        {
            base.EnterState();
            //_blackBoard.playerMovement.transform.DOLookAt(_blackBoard._targetEnemy.transform.position, 0.2f, AxisConstraint.Y);
        }

        public override StateStatus UpdateState()
        {
            base.UpdateState();

            if (_state.NormalizedTime >= 1)
            {
                _fsm.ChangeState(_idleCombat);
                return StateStatus.Success;
            }

            return StateStatus.Running;
        }

        public override void ExitState()
        {
            base.ExitState();
        }
    }
}