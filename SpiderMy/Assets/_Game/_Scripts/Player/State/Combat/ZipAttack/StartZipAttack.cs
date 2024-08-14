using _Game.Scripts.Event;
using DG.Tweening;
using EasyCharacterMovement;
using SFRemastered._Game._Scripts.Enemy;
using SFRemastered._Game._Scripts.State.Combat;
using UnityEngine;
using UnityEngine.Serialization;

namespace SFRemastered.Combat.ZipAttack
{
    [CreateAssetMenu(menuName = "ScriptableObjects/States/CombatStates/StartZipAttack")]
    public class StartZipAttack : CombatBase
    {
        [SerializeField] private ZipGroundAttack zipGroundAttack;
        [SerializeField] private ZipAirAttack _zipAirAttack;
        
        public override void EnterState()
        {
            base.EnterState();
            _blackBoard.playerMovement.SetMovementDirection(Vector3.zero);
            _blackBoard.rigidbody.interpolation = RigidbodyInterpolation.None;
            _blackBoard.enemyInRange.FindClosestEnemy().gameObject.GetComponent<EnemyController>().zipAttackStun = true;
            _blackBoard.playerMovement.transform.DOLookAt(_blackBoard.enemyInRange.FindClosestEnemy().transform.position, 0.2f, AxisConstraint.Y);
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
            
        }
    }
}