using DG.Tweening;
using SFRemastered._Game._Scripts.State.Combat.IdleCombat;
using UnityEngine;
using UnityEngine.Serialization;

namespace SFRemastered.Combat.ZipAttack
{
    [CreateAssetMenu(menuName = "ScriptableObjects/States/CombatStates/EndZipAttack")]
    public class EndZipAttack : StateBase
    {
        [FormerlySerializedAs("normalIdleCombatBase")] [FormerlySerializedAs("idleIdlesCombat")] [FormerlySerializedAs("_idleCombat")] [SerializeField] private NormalIdleCombat normalIdleCombat;

        public override void EnterState()
        {
            base.EnterState();
            _blackBoard.playerMovement.SetMovementDirection(Vector3.zero);
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