using DG.Tweening;
using SFRemastered._Game._Scripts.State.Combat;
using SFRemastered._Game._Scripts.State.Combat.IdleCombat;
using SFRemastered._Game.ScriptableObjects.AnimationAttack;
using UnityEngine;
using UnityEngine.Serialization;

namespace SFRemastered.Combat.ZipAttack
{
    [CreateAssetMenu(menuName = "ScriptableObjects/States/CombatStates/EndZipAttack")]
    public class EndZipAttack : CombatBase
    {
        [SerializeField] private NormalIdleCombat normalIdleCombat;
        [SerializeField] private AttackAnim attackAnim;
        

        public override void EnterState()
        {
            base.EnterState();
            _blackBoard.playerMovement.SetMovementDirection(Vector3.zero);
            _currentDamage = attackAnim.damage;
            _state = _blackBoard.animancer.Play(attackAnim.clip);
            _state.Events.SetCallback("Hit", GetHit);
        }

        public override StateStatus UpdateState()
        {
            base.UpdateState();

            if (_state.NormalizedTime >= 1)
            {
                _fsm.ChangeState(normalIdleCombat);
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