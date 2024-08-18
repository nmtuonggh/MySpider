﻿using DG.Tweening;
using SFRemastered._Game._Scripts.Player.State.Combat.Gadget;
using SFRemastered._Game._Scripts.State.Locomotion.Ground;
using SFRemastered._Game.ScriptableObjects.AnimationAttack;
using SFRemastered.OnHitState;
using UnityEngine;

namespace SFRemastered._Game._Scripts.State.Combat
{
    public abstract class CombatBase : StateBase
    {
        [SerializeField] protected DodgeState _dodgeState;
        [SerializeField] protected StaggerState staggerState;
        [SerializeField] protected KnockBackState knockBackState;
        
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

            if (_blackBoard.staggerHit)
            {
                _fsm.ChangeState(staggerState);
                return StateStatus.Success;
            }

            if (_blackBoard.knockBackHit)
            {
                _fsm.ChangeState(knockBackState);
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
                _blackBoard.overlapSphereHit.Hit( _currentDamage);
            }
        }
        
        public void GetKnockBackHit()
        {
            _blackBoard.overlapSphereHit.KnockBackHit(_currentDamage);
        }
        
    }
}