using DG.Tweening;
using SFRemastered._Game._Scripts.Enemy.State;
using SFRemastered._Game._Scripts.State.Combat;
using SFRemastered._Game._Scripts.State.Combat.IdleCombat;
using UnityEngine;

namespace SFRemastered._Game._Scripts.Player.State.Combat.Gadget
{
    [CreateAssetMenu(menuName = "ScriptableObjects/States/CombatStates/WebShooter")]
    public class WebShooter : GadgetBase
    {
        [SerializeField] private NormalIdleCombat _normalIdleCombat;
        [SerializeField] private int stack = 0;

        private GameObject target;

        public override void EnterState()
        {
            target = _blackBoard.enemyInRange.FindClosestEnemyNotStun();
            if (target != null)
            {
                _blackBoard.playerMovement.transform.DOLookAt(target.transform.position, 0.2f, AxisConstraint.Y);
            }

            base.EnterState();
            _state.Time = 0;
            _blackBoard.playerMovement.SetMovementDirection(Vector3.zero);
        }

        public override StateStatus UpdateState()
        {
            StateStatus baseStatus = base.UpdateState();
            if (baseStatus != StateStatus.Running)
            {
                return baseStatus;
            }

            _blackBoard.playerMovement.SetMovementDirection(Vector3.zero);
            
            if (_state.NormalizedTime >= 1f)
            {
                Debug.Log("Change state");
                _fsm.ChangeState(_normalIdleCombat);
                return StateStatus.Success;
            }

            return StateStatus.Running;
        }

        public override void ExitState()
        {
            base.ExitState();
        }

        public void ShootWebLeft()
        {
            if (target != null)
            {
                var rotation = Quaternion.LookRotation(target.transform.position - _blackBoard.startrope.position);

                var web = _blackBoard.projectileWebShooterSo.Spawn(_blackBoard.startrope.position, rotation,
                    _blackBoard.poolManager.transform);

                web.transform.DOMove(target.transform.position + new Vector3(0, 0.7f, 0), 0.2f).OnComplete(() =>
                {
                    //target.GetComponent<EnemyBlackBoard>().webHitStun += 1;
                    //target.GetComponent<EnemyBlackBoard>().stunLockHit = true;
                    _blackBoard.projectileWebShooterSo.ReturnToPool(web);
                });
            }
            else
            {
                var web = _blackBoard.projectileWebShooterSo.Spawn(_blackBoard.startrope.position,
                    _blackBoard.playerMovement.transform.rotation,
                    _blackBoard.poolManager.transform);

                web.transform.DOMove(
                    (_blackBoard.playerMovement.transform.position + _blackBoard.transform.forward * 15) + new Vector3(
                        0, 0.5f, 0), 0.2f).OnComplete(() => { _blackBoard.projectileWebShooterSo.ReturnToPool(web); });
            }
        }

        public void ShootWebRight()
        {
            if (target != null)
            {
                var rotation = Quaternion.LookRotation(target.transform.position - _blackBoard._zipAttackHandPositon.position);
                var web = _blackBoard.projectileWebShooterSo.Spawn(_blackBoard._zipAttackHandPositon.position, rotation,
                    _blackBoard.poolManager.transform);
                web.transform.DOMove(target.transform.position + new Vector3(0, 0.7f, 0), 0.2f).OnComplete(() =>
                {
                    //target.GetComponent<EnemyBlackBoard>().webHitStun += 1; 
                    target.GetComponent<EnemyBlackBoard>().stunLockHit = true;
                    _blackBoard.projectileWebShooterSo.ReturnToPool(web);
                });
            }
            else
            {
                var web = _blackBoard.projectileWebShooterSo.Spawn(_blackBoard._zipAttackHandPositon.position,
                    _blackBoard.playerMovement.transform.rotation,
                    _blackBoard.poolManager.transform);

                web.transform.DOMove(
                    (_blackBoard.playerMovement.transform.position + _blackBoard.transform.forward * 15) + new Vector3(
                        0, 0.5f, 0), 0.2f).OnComplete(() => { _blackBoard.projectileWebShooterSo.ReturnToPool(web); });
            }
        }
    }
}