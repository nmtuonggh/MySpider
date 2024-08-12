using EasyCharacterMovement;
using SFRemastered._Game._Scripts.State.Combat;
using UnityEngine;

namespace SFRemastered.Combat.ZipAttack
{
    public abstract class ZipAttack : CombatBase
    {
        [SerializeField] protected EndZipAttack _endZipAttack;
        protected bool _doneMove = false;

        public override void EnterState()
        {
            base.EnterState();
        }

        public override StateStatus UpdateState()
        {
            base.UpdateState();

            if (_doneMove)
            {
                _fsm.ChangeState(_endZipAttack);
                return StateStatus.Success;
            }

            return StateStatus.Running;
        }

        public override void ExitState()
        {
            base.ExitState();
            _doneMove = false;
        }

        protected void DrawnWeb(Vector3 start, Vector3 end)
        {
            _blackBoard.lr.positionCount = 2;
            _blackBoard.lr.SetPosition(1, end);
            _blackBoard.lr.SetPosition(0, start);
        }
    }
}