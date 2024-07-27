using EasyCharacterMovement;
using UnityEngine;

namespace SFRemastered.Combat.ZipAttack
{
    public abstract class ZipAttack : StateBase
    {
        [SerializeField] protected EndZipAttack _endZipAttack;
        protected bool _doneMove = false;

        public override void EnterState()
        {
            base.EnterState();
            _blackBoard.playerMovement.SetMovementMode(MovementMode.None);
            _blackBoard.rigidbody.constraints =
                RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
            _blackBoard.rigidbody.useGravity = false;
            _blackBoard.rigidbody.isKinematic = false;
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
            _blackBoard.playerMovement.SetMovementMode(MovementMode.Walking);
            _blackBoard.rigidbody.useGravity = false;
            _blackBoard.rigidbody.isKinematic = true;
            _blackBoard.rigidbody.constraints = RigidbodyConstraints.None;
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