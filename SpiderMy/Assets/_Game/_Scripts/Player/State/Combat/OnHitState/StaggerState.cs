using SFRemastered._Game._Scripts.State.Combat.IdleCombat;
using UnityEngine;

namespace SFRemastered.OnHitState
{
    [CreateAssetMenu(menuName = "ScriptableObjects/States/CombatStates/Stagger")]

    public class StaggerState : StateBase
    {
        [SerializeField] private NormalIdleCombat normalIdleCombat;
        public override void EnterState()
        {
            base.EnterState();
            _blackBoard.staggerHit = false;
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