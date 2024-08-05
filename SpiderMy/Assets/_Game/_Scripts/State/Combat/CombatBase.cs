using SFRemastered._Game._Scripts.State.Locomotion.Ground;
using SFRemastered._Game.ScriptableObjects.AnimationAttack;
using UnityEngine;

namespace SFRemastered._Game._Scripts.State.Combat
{
    public abstract class CombatBase : StateBase
    {
        [SerializeField] protected DodgeState _dodgeState;
        [SerializeField] protected UltimateSkill _ultimateSkill;
        
        protected float _currentDamage;
        public override void EnterState()
        {
            base.EnterState();
        }
        
        public override StateStatus UpdateState()
        {
            StateStatus baseStatus = base.UpdateState();
            if(baseStatus != StateStatus.Running)
            {
                return baseStatus;
            }

            if (_blackBoard.dodge)
            {
                _fsm.ChangeState(_dodgeState);
                return StateStatus.Success;
            }

            if (_blackBoard.ultimate)
            {
                _fsm.ChangeState(_ultimateSkill);
                return StateStatus.Success;
            }
            
            return StateStatus.Running;
        }
        
        public override void ExitState()
        {
            base.ExitState();
        }
        
        public void GetHit()
        {
            _blackBoard.overlapSphereHit.Hit(_currentDamage);
        }
        
    }
}