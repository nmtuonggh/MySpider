using DG.Tweening;
using EasyCharacterMovement;
using UnityEngine;
using UnityEngine.Serialization;

namespace SFRemastered.Combat.ZipAttack
{
    [CreateAssetMenu(menuName = "ScriptableObjects/States/CombatStates/StartZipAttack")]
    public class StartZipAttack : StateBase
    {
        [FormerlySerializedAs("_zipAttack")] [SerializeField] private ZipGroundAttack zipGroundAttack;
        [SerializeField] private ZipAirAttack _zipAirAttack;
        public override void EnterState()
        {
            base.EnterState();
            
            _blackBoard.playerMovement.transform.DOLookAt(_blackBoard._targetEnemy.transform.position, 0.2f, AxisConstraint.Y);
            _blackBoard.playerMovement.SetMovementMode(MovementMode.None);
            _blackBoard.rigidbody.constraints =
                RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
            _blackBoard.rigidbody.useGravity = false; 
            _blackBoard.rigidbody.isKinematic = false;
        }

        public override StateStatus UpdateState()
        {
            base.UpdateState();

            if (_state.NormalizedTime >= 1)
            {
                if (_blackBoard.playerMovement.IsGrounded())
                {
                    _fsm.ChangeState(zipGroundAttack);
                }
                else
                {
                    _fsm.ChangeState(_zipAirAttack);
                }
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
        }
    }
}