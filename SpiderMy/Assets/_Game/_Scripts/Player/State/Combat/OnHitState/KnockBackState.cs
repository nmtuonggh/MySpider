using SFRemastered._Game._Scripts.Player.State.Combat;
using SFRemastered._Game._Scripts.State.Combat.IdleCombat;
using UnityEngine;

namespace SFRemastered.OnHitState
{
    [CreateAssetMenu(menuName = "ScriptableObjects/States/CombatStates/KnockBack")]

    public class KnockBackState : StateBase
    {
        [SerializeField] private Riseup riseupState;
        public override void EnterState()
        {
            base.EnterState();
            _blackBoard.invincible = true;
            _blackBoard.playerMovement.useRootMotion = true;
            //_blackBoard.knockBackHit = false;
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
                _fsm.ChangeState(riseupState);
                return StateStatus.Success;
            }

            return StateStatus.Running;
        }
        
        public override void ExitState()
        {
            base.ExitState();
            _blackBoard.invincible = false;
            _blackBoard.playerMovement.useRootMotion = false;
        }
    }
}