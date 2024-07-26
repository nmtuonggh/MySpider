using DG.Tweening;
using EasyCharacterMovement;
using UnityEngine;

namespace SFRemastered.Combat.ZipAttack
{
    [CreateAssetMenu(menuName = "ScriptableObjects/States/CombatStates/ZipAttack")]
    public class ZipAttack : StateBase
    {
        [SerializeField] private EndZipAttack _endZipAttack;
        
        private bool _doneMove = false;
        
        public override void EnterState()
        {
            base.EnterState();
            //var playerPlanPos = new Vector3(_blackBoard.transform.position.x, _blackBoard._targetEnemy.transform.position.y, _blackBoard.transform.position.z);
            //var targetPos = (_blackBoard._targetEnemy.transform.position - playerPlanPos).normalized;
            var targetPos = (_blackBoard.transform.position - _blackBoard._targetEnemy.transform.position).normalized;
            _blackBoard.playerMovement.transform
                .DOMove((_blackBoard._targetEnemy.transform.position + targetPos * 1f) + new Vector3(0,0.1f,0), 0.35f).OnComplete(
                    () =>
                    {
                        _doneMove = true;
                        _blackBoard.lr.positionCount = 0;
                    });
            _blackBoard.playerMovement.SetMovementMode(MovementMode.None);
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
    }
}