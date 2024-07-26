﻿using DG.Tweening;
using EasyCharacterMovement;
using UnityEngine;

namespace SFRemastered.Combat.ZipAttack
{
    [CreateAssetMenu(menuName = "ScriptableObjects/States/CombatStates/StartZipAttack")]
    public class StartZipAttack : StateBase
    {
        [SerializeField] private ZipAttack _zipAttack;
        [SerializeField] private ZipAirAttack _zipAirAttack;
        public override void EnterState()
        {
            base.EnterState();
            
            _blackBoard.playerMovement.transform.DOLookAt(_blackBoard._targetEnemy.transform.position, 0.2f, AxisConstraint.Y).OnComplete(
                () =>
                {
                    DrawnWeb();
                });
            _blackBoard.playerMovement.SetMovementMode(MovementMode.None);
            _blackBoard.rigidbody.constraints =
                RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
            _blackBoard.rigidbody.useGravity = false; 
            _blackBoard.rigidbody.isKinematic = false;
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
                if (_blackBoard.playerMovement.IsGrounded())
                {
                    _fsm.ChangeState(_zipAttack);
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