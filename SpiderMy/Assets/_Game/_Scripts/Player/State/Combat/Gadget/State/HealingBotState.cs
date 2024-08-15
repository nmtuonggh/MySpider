using System.Collections.Generic;
using DG.Tweening;
using SFRemastered._Game._Scripts.State.Combat.IdleCombat;
using UnityEngine;

namespace SFRemastered._Game._Scripts.Player.State.Combat.Gadget
{
    namespace SFRemastered._Game._Scripts.Player.State.Combat.Gadget
    {
        [CreateAssetMenu(menuName = "ScriptableObjects/States/CombatStates/HeallingBot")]
        public class HealingBotState : GadgetBase
        {
            [SerializeField] private ProjectileHealBotSO _projectileHealBotSo;
            [SerializeField] private NormalIdleCombat _normalIdleCombat;
            
            private GameObject healingBot;

            public override void EnterState()
            {
                base.EnterState();
                _state.Time = 0;
            }

            public override StateStatus UpdateState()
            {
                _blackBoard.playerMovement.SetMovementDirection(Vector3.zero);
                StateStatus baseStatus = base.UpdateState();
                if (baseStatus != StateStatus.Running)
                {
                    return baseStatus;
                }

                return StateStatus.Running;
            }

            public override void ExitState()
            {
                base.ExitState();
            }

            public void ShotProjectile()
            {
                var rotation = Quaternion.LookRotation(_blackBoard.projectileHealingBotPosition.transform.position -
                                                       _blackBoard._zipAttackHandPositon.position);
                var projectile = _projectileHealBotSo.Spawn(_blackBoard._zipAttackHandPositon.position, rotation,
                    _blackBoard.poolManager.transform);
                projectile.transform.DOMove(_blackBoard.projectileHealingBotPosition.position, 0.5f).OnComplete(() =>
                {
                    _projectileHealBotSo.ReturnToPool(projectile);
                    _blackBoard.healingBot.SetActive(true);
                    _fsm.ChangeState(_normalIdleCombat);
                });
            }
        }
    }
}