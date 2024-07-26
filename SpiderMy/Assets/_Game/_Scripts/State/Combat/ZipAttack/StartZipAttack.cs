using DG.Tweening;
using UnityEngine;

namespace SFRemastered.Combat.ZipAttack
{
    [CreateAssetMenu(menuName = "ScriptableObjects/States/CombatStates/StartZipAttack")]
    public class StartZipAttack : StateBase
    {
        [SerializeField] private ZipAttack _zipAttack;
        public override void EnterState()
        {
            base.EnterState();
            _blackBoard.playerMovement.transform.DOLookAt(_blackBoard._targetEnemy.transform.position, 0.2f, AxisConstraint.Y).OnComplete(
                () =>
                {
                    DrawnWeb();
                });
        }

        private void DrawnWeb()
        {
            _blackBoard.lr.positionCount = 2;
            _blackBoard.lr.SetPosition(1, _blackBoard._targetEnemy.transform.position);
            _blackBoard.lr.SetPosition(0, _blackBoard._zipAttackHandPositon.position);
        }

        public override StateStatus UpdateState()
        {
            base.UpdateState();

            if (_state.NormalizedTime >= 1)
            {
                _fsm.ChangeState(_zipAttack);
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