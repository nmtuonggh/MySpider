using DG.Tweening;
using SFRemastered._Game._Scripts.Player.State.Combat.Gadget;
using SFRemastered._Game._Scripts.State.Locomotion.Ground;
using SFRemastered._Game.ScriptableObjects.AnimationAttack;
using UnityEngine;

namespace SFRemastered._Game._Scripts.State.Combat
{
    public abstract class CombatBase : StateBase
    {
        [SerializeField] protected DodgeState _dodgeState;
        
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
            
            return StateStatus.Running; 
        }
        
        public override void ExitState()
        {
            base.ExitState();
        }
        
        public void GetNormalHit()
        {
            if (_blackBoard._detectedEnemy)
            {
               
                _blackBoard.overlapSphereHit.Hit(BlackBoard.HitType.stagger, _currentDamage);
            }
        }
        
        public void GetMidRangeHit()
        {
            _blackBoard.overlapSphereHit.Hit(BlackBoard.HitType.stagger, _currentDamage);
        }
        
    }
}